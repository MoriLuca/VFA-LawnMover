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
    private bool IsMoving;
    public LayerMask SceneObjectsLayer;
    public float MovementStep = 1;
    public float MoveSpeed = 3;
    private Vector2 _input;
    private int _index_size_x = 1;
    private int _index_size_y = 1;
    private int[] _availableSizes = {0,1,3,5};
    

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
        if(Input.GetKeyDown(KeyCode.Z)) SizeDown();
        if(Input.GetKeyDown(KeyCode.X)) SizeUp();
        if(Input.GetKeyDown(KeyCode.R)) RepaintRequest();
        if(Input.GetKeyDown(KeyCode.T)) RandomizeSize();
        if(Input.GetKeyDown(KeyCode.G)) ChangeGraficStyle();

        if (!IsMoving)
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");


            if (_input != Vector2.zero )
            {   
                

                var moveRequestX = _input.x != 0;
                var moveRequestY = _input.y != 0;
                var moveStepX = MovementStep * _input.x;
                var moveStepY = MovementStep * _input.y;
                var targetPosition = transform.position;
                if( moveRequestX ) moveStepY = 0;

                if(_input.x != 0) transform.rotation = Quaternion.AngleAxis((_input.x > 0)?270f:90f, Vector3.forward);
                else transform.rotation = Quaternion.AngleAxis((_input.y > 0)?0f:180f, Vector3.forward);

                targetPosition.x += moveStepX;
                targetPosition.y += moveStepY;
                if(IsWalkable(targetPosition))
                {
                    StartCoroutine(Move(targetPosition));
                    var vector2Int = new Vector2Int((int)targetPosition.x, (int)targetPosition.y);
                    //print($"player moved in x: {vector2Int.x} y: {vector2Int.y}");
                }
                
            }
        }
    }

    private void ChangeGraficStyle()
    {
        _groundManager.ChangeGraficStyle();
    }

    public void SizeDown()
    {
        _index_size_x--;
        if(_index_size_x<1) _index_size_x=1;
        _index_size_y--;
        if(_index_size_y<1) _index_size_y=1;
        ApplySize();
    }
    public void SizeUp(){
        _index_size_x++;
        if(_index_size_x<0) _index_size_x=0;
        _index_size_y++;
        if(_index_size_y<0) _index_size_y=0;
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

    }

    private void ApplySize()
    {
        gameObject.transform.localScale = new Vector3(_availableSizes[_index_size_x],_availableSizes[_index_size_y],0);
        MapMoveSpeed();
    }

    private void RepaintRequest() => _groundManager.GenerateGround();

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


    IEnumerator Move(Vector3 targetPosition)
    {
        IsMoving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;

        IsMoving = false;

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
