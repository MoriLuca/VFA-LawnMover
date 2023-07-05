using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    private Logger _log;

    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void JustCreatedHandler(object sender, object args)
    {
        GameHandler.Istance.Logger.Trace(this, "Hello newborn");
    }
}
