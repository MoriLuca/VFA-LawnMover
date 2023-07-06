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
    private List<GameObject> _elencoZolle = new List<GameObject>();
    private WorldStyle _worldStyle;
    public Sprite[] GroundStyleInUse = new Sprite[3];
    private Sprite[,] GroundStyleSprites = new Sprite[2,3];
    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");

        //plain style
        _zolla = Resources.Load<GameObject>("Prefabs/Ground/Ground");
        GroundStyleSprites[0,0] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage1");
        GroundStyleSprites[0,1] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage2");
        GroundStyleSprites[0,2] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage3");

        if (GroundStyleSprites[0,0] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[0,1] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[0,2] == null){throw new FileNotFoundException("File non trovato");}

        GroundStyleInUse[0] = GroundStyleSprites[0,0];
        GroundStyleInUse[1] = GroundStyleSprites[0,1];
        GroundStyleInUse[2] = GroundStyleSprites[0,2];

        Initialized = true;
    }

    public void GenerateGround()
    {
        _log.Trace(this, "inizio la creazione del terreno");
        for (int i = 0; i < DimensioniCampoX; i++)
        {
            for (int j = 0; j < DimensioniCampoY; j++)
            {
                var zolla = Instantiate(_zolla, new Vector3(i, j, 0), Quaternion.identity);
                zolla.GetComponent<SpriteRenderer>().sprite = GroundStyleInUse[2];
                _elencoZolle.Add(zolla);
            }
        }
    }

    public void ChangeGraficStyle()
    {
        _log.Trace(this, "Cambio di style richiesto");
        if((int)_worldStyle == Enum.GetValues(typeof(WorldStyle)).Length -1)
        {
            _worldStyle++;
        }
        else{
            _worldStyle = WorldStyle.Plain;
        }

        GroundStyleInUse[0] = GroundStyleSprites[(int)_worldStyle, 0];
        GroundStyleInUse[1] = GroundStyleSprites[(int)_worldStyle, 1];
        GroundStyleInUse[2] = GroundStyleSprites[(int)_worldStyle, 2];

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
    Plain = 0,
    BadPixelArt
}
