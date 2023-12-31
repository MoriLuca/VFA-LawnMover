using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameHandler _gameHandler;
    private Logger _log;
    private GroundManager _groundManager;
    public bool Initialized { get; private set; }
    public event EventHandler JustCreated;
    public event EventHandler RefreshGameInfoUIEvent;
    public event EventHandler<Vector2Int> ItemDiscardedEvent;
    private bool IsMoving;
    public LayerMask SceneObjectsLayer;
    public float MovementStep = 1;
    public float MoveSpeed = 10;
    private Vector2 _input;
    private int _index_size_x = 1;
    private int _index_size_y = 1;
    private int[] _availableSizes = {0,1,3,5,7,9,11};
    public DisposableItemBase ItemInPoket;
    private int _nosRemainigStep = 0;

    

    void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _groundManager = _gameHandler.GroundManager;
        _log.Debug(this, "g manager");
        _log.Debug(this, _groundManager.ToString());
        JustCreated?.Invoke(this,null);
        Initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) PlayerAction();
        if(Input.GetKeyDown(KeyCode.X)) UseItem();
        if(Input.GetKeyDown(KeyCode.R)) RepaintRequest();
        if(Input.GetKeyDown(KeyCode.T)) RandomizeSize();
        if(Input.GetKeyDown(KeyCode.G)) ChangeGraficStyle();
        if(Input.GetKeyDown(KeyCode.C)) DiscardItem();

        if (!IsMoving)
        {
            var movementStep = MovementStep;
            var speed = MoveSpeed;

            if(_nosRemainigStep>0)
            {
                movementStep = 4;
                speed = MoveSpeed + 20;
            }
            
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");


            if (_input != Vector2.zero )
            {   
                
                var moveRequestX = _input.x != 0;
                var moveRequestY = _input.y != 0;
                var moveStepX = movementStep * _input.x;
                var moveStepY = movementStep * _input.y;
                var targetPosition = transform.position;
                if( moveRequestX ) moveStepY = 0;

                if(_input.x != 0) transform.rotation = Quaternion.AngleAxis((_input.x > 0)?270f:90f, Vector3.forward);
                else transform.rotation = Quaternion.AngleAxis((_input.y > 0)?0f:180f, Vector3.forward);

                targetPosition.x += moveStepX;
                targetPosition.y += moveStepY;
                if(IsWalkable(targetPosition))
                {
                    StartCoroutine(Move(targetPosition, speed));
                    var vector2Int = new Vector2Int((int)targetPosition.x, (int)targetPosition.y);
                    //print($"player moved in x: {vector2Int.x} y: {vector2Int.y}");
                }
                
            }
        }
    }

    public void Teleport()
    {
        var go = _groundManager.ElencoZolle[UnityEngine.Random.Range(0,_groundManager.ElencoZolle.Count)];
        var zollaPosition = go.transform.position;
        gameObject.transform.position = zollaPosition;
    }

    public void IncreseNOS(int nosStepToIncrease)
    {
        _nosRemainigStep += nosStepToIncrease;
    }

    private void PlayerAction()
    {

    }

    public bool HasCapsuleAvailable()
    {
        return ItemInPoket is not null;
    }

    private void UseItem()
    {
        var item = ItemInPoket;
        if(item != null)
        {
            _log.Trace(this, "Im using the object");
            item.Use();
            ItemInPoket = null;
            
        }
        TriggerGameInfoUIRefresh();
    }

    private void DiscardItem()
    {
        var item = ItemInPoket;
        if(item != null)
        {
            _log.Trace(this, "Im descarding the item");
            ItemInPoket = null;
            ItemDiscardedEvent?.Invoke(
                this, new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y));
        }
        TriggerGameInfoUIRefresh();
    }

    private void ChangeGraficStyle()
    {
        _groundManager.ChangeGraficStyle();
        TriggerGameInfoUIRefresh();
    }
    public void SpeedUp()
    {
        MoveSpeed ++;
    }
    public void SpeedDown()
    {
        MoveSpeed --;
        if(MoveSpeed < 1) MoveSpeed =1;
    }

    public void TriggerGameInfoUIRefresh()
    {
        RefreshGameInfoUIEvent?.Invoke(this, null);
    }
    public void SizeDown()
    {
        _index_size_x--;
        _index_size_y--;
        if(_index_size_x<1) _index_size_x=1;
        if(_index_size_y<1) _index_size_y=1;
        ApplySize();
    }
    public void SizeUp(){
        var maxIndex = _availableSizes.Length-1;
        _index_size_x++;
        _index_size_y++;
        if(_index_size_x>maxIndex) _index_size_x=maxIndex;
        if(_index_size_y>maxIndex) _index_size_y=maxIndex;
        ApplySize();
    }

    public void RandomizeSize(){
        _index_size_x = UnityEngine.Random.Range(1,4);
        _index_size_y = UnityEngine.Random.Range(1,4);
        ApplySize();
    }
    private void MapMoveSpeed()
    {
        var output_end = 3;
        var output_start = 25;
        var input_end = 4;
        var input_start = 0;
        var input = (_index_size_x > _index_size_y)?_index_size_x:_index_size_y;

        MoveSpeed = output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start);
        MoveSpeed = 15;
    }

    private void ApplySize()
    {
        gameObject.transform.localScale = new Vector3(_availableSizes[_index_size_x],_availableSizes[_index_size_y],0);
        TriggerGameInfoUIRefresh();
    }

    private void RepaintRequest()
    {
        print("richiesta repaint");
        _groundManager.GenerateGround();
    } 

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        _log.Trace(this, "something found");
        if (collider.gameObject.TryGetComponent(out GroundCollisionHandler ground))
        {
            _log.Trace(this, "player stepped on ground");
            ground.CollisionFromPlayerHandler();
            //Destroy(gameObject);
        }
        
    }


    IEnumerator Move(Vector3 targetPosition, float speed)
    {
        IsMoving = true;
        if(_nosRemainigStep>0) _nosRemainigStep--;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;

        IsMoving = false;
        TriggerGameInfoUIRefresh();

    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, 0.3f, SceneObjectsLayer) != null)
        {
            return false;
        }
        return true;
        
    }
}
