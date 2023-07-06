using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private Logger _log;
    public Object _zolla;
    public bool Initialized = false;
    public int DimensioniCampoX = 50;
    public int DimensioniCampoY = 20;

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
        for (int i = 0; i < DimensioniCampoX; i++)
        {
            for (int j = 0; j < DimensioniCampoY; j++)
            {
                var zolla = Instantiate(_zolla, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _=false;
    }
}
