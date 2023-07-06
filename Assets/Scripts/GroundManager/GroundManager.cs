using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private Logger _log;
    public GameObject _zolla;
    public bool Initialized = false;
    public int DimensioniCampoX = 50;
    public int DimensioniCampoY = 20;
    private Sprite _flower;
    private Sprite _soil;
    private List<GameObject> _elencoZolle;
    private WorldStyle _worldStyle;
    public GameObject[,] WorldStylesObjects = new GameObject[2,3];

    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");

        //plain style
        var r = Resources.Load<GameObject>("Prefabs/Ground/PlainStyle/Step1");
        print(r);
        WorldStylesObjects[0,0] = Resources.Load<GameObject>("Prefabs/Ground/PlainStyle/Step1");
        // WorldStylesObjects[0,1] = Resources.Load<GameObject>("Resources/Prefabs/Ground/PlainStyle/Step1");
        // WorldStylesObjects[0,2] = Resources.Load<GameObject>("Resources/Prefabs/Ground/PlainStyle/Step1");
        if (WorldStylesObjects[0,0] == null){throw new FileNotFoundException("File non trovato");}
        // if (WorldStylesObjects[0,1] == null){throw new FileNotFoundException("File non trovato");}
        // if (WorldStylesObjects[0,2] == null){throw new FileNotFoundException("File non trovato");}
        Initialized = true;
    }

    public void GenerateGround()
    {
        _log.Trace(this, "inizio la creazione del terreno");
        for (int i = 0; i < DimensioniCampoX; i++)
        {
            for (int j = 0; j < DimensioniCampoY; j++)
            {
                var zolla = Instantiate(WorldStylesObjects[0,0], new Vector3(i, j, 0), Quaternion.identity);
                _elencoZolle.Add(zolla);
            }
        }
    }

    public void ChangeGraficStyle()
    {
        _log.Trace(this, "Cambio di style richiesto");
        if((int)_worldStyle == Enum.GetValues(typeof(WorldStyle)).Length)
        {
            _worldStyle++;
        }
        else{
            _worldStyle = WorldStyle.Plain;
        }
        foreach (var zolla in _elencoZolle)
        {
            SwapZollaStyle(zolla);
        }
    }

    private void SwapZollaStyle(GameObject zolla)
    {
        // Sprite toBeUsed = _zolla;
        // zolla.transform.GetComponent<SpriteRenderer>().sprite = toBeUsed;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

public enum WorldStyle
{
    Plain = 1,
    BadPixelArt
}
