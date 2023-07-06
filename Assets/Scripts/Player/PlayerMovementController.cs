using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private bool IsMoving;
    public LayerMask SceneObjectsLayer;
    public float MovementStep = 1;
    public float MoveSpeed = 3;
    private Vector2 _input;
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                    print($"player moved in x: {vector2Int.x} y: {vector2Int.y}");
                }
                
            }
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
