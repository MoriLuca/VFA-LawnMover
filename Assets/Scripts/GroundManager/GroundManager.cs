using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private Logger _log;
    public Object _zolla;
    public bool Initialized = false;

    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");
        _zolla = Resources.Load("Prefabs/Ground/" + "Square");
        _log.Trace(this, _zolla.ToString());
        _log.Trace(this, "file caricato");
        if (_zolla == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        Initialized = true;
    }

    public void GenerateGround()
    {
        _log.Trace(this, "inizio la creazione del terreno");
        _log.Trace(this, _zolla.ToString());
        Instantiate(_zolla, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(_zolla, new Vector3(0, 1, 0), Quaternion.identity);
        Instantiate(_zolla, new Vector3(0, 2, 0), Quaternion.identity);
        Instantiate(_zolla, new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(_zolla, new Vector3(1, 1, 0), Quaternion.identity);
        Instantiate(_zolla, new Vector3(1, 2, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
