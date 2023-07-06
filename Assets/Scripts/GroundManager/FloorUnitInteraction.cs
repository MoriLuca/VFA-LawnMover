using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUnitInteraction : MonoBehaviour
{
    private Logger _log;

    private void Start() 
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Zolla creata");
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.TryGetComponent(out PlayerMovementController player))
        {
            
            //Destroy(gameObject);
        }
        
    }
}



