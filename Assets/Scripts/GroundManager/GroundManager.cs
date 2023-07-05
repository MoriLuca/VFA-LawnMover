using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
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
}
