using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public event EventHandler JustCreated;
    private Logger _log;


    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");
        JustCreated?.Invoke(this,null);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
