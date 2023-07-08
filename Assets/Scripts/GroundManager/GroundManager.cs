using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private Logger _log;
    public GameObject _zolla;
    public bool Initialized = false;
    public int DimensioniCampoX = 50;
    public int DimensioniCampoY = 20;
    public  List<GameObject> ElencoZolle {get; private set;} = new List<GameObject>();
    private WorldStyle _worldStyle;
    public Sprite[] GroundStyleInUse = new Sprite[3];
    private Sprite[,] GroundStyleSprites = new Sprite[3,3];
    void Start()
    {
        _log = GameHandler.Istance.Logger;
        _log.Trace(this, "Created");
        _zolla = Resources.Load<GameObject>("Prefabs/Ground/Ground");

        //plain style
        GroundStyleSprites[0,0] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage1");
        GroundStyleSprites[0,1] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage2");
        GroundStyleSprites[0,2] = Resources.Load<Sprite>("Gfx/Png/Ground/StylePlain/Stage3");
        if (GroundStyleSprites[0,0] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[0,1] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[0,2] == null){throw new FileNotFoundException("File non trovato");}
        //bad pixel art
        GroundStyleSprites[1,0] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelArtBad/Stage1");
        GroundStyleSprites[1,1] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelArtBad/Stage2");
        GroundStyleSprites[1,2] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelArtBad/Stage3");
        if (GroundStyleSprites[1,0] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[1,1] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[1,2] == null){throw new FileNotFoundException("File non trovato");}
        //pixel snow
        GroundStyleSprites[2,0] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelSnow/Stage1");
        GroundStyleSprites[2,1] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelSnow/Stage2");
        GroundStyleSprites[2,2] = Resources.Load<Sprite>("Gfx/Png/Ground/PixelSnow/Stage3");
        if (GroundStyleSprites[1,0] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[1,1] == null){throw new FileNotFoundException("File non trovato");}
        if (GroundStyleSprites[1,2] == null){throw new FileNotFoundException("File non trovato");}


        GroundStyleInUse[0] = GroundStyleSprites[0,0];
        GroundStyleInUse[1] = GroundStyleSprites[0,1];
        GroundStyleInUse[2] = GroundStyleSprites[0,2];

        Initialized = true;
    }

    public void GenerateGround()
    {
        print("genero");
        _log.Trace(this, "inizio la creazione del terreno");
        if(ElencoZolle.Count > 0) 
        {
            foreach (var x in ElencoZolle)
            {
                Destroy(x);
            }
        }
        ElencoZolle.Clear();

        for (int i = 0; i < DimensioniCampoX; i++)
        {
            for (int j = 0; j < DimensioniCampoY; j++)
            {
                var zolla = Instantiate(_zolla, new Vector3(i, j, 0), Quaternion.identity);
                zolla.GetComponent<SpriteRenderer>().sprite = GroundStyleInUse[0];
                ElencoZolle.Add(zolla);
            }
        }
    }


    public List<GameObject> FindZolleForExplosion(Vector2Int position)
    {
        var explosionRangeX = UnityEngine.Random.Range(0,5);
        var explosionRangeY = UnityEngine.Random.Range(0,5);

        var zolle = ElencoZolle.Where(z=> 
            ( (position.x - explosionRangeX) <= z.transform.position.x &&  z.transform.position.x <= ( position.x + explosionRangeX) ) && 
            ( (position.y - explosionRangeY) <= z.transform.position.y &&  z.transform.position.y <= ( position.y + explosionRangeY) )).ToList();

        return zolle;
    }


    public void ItemDiscardedEventHandler(object sender, Vector2Int position)
    {
        _log.Debug(this, "evento discard item");
        var zolleEspolosione = FindZolleForExplosion(position);

        foreach (var zolla in zolleEspolosione)
        {

            zolla.GetComponent<GroundCollisionHandler>().Steps = 2;
            zolla.GetComponent<GroundCollisionHandler>().ForceRefresh();
        }
    }

    public void ChangeGraficStyle()
    {
        _log.Trace(this, "Cambio di style richiesto");
        if((int)_worldStyle < Enum.GetValues(typeof(WorldStyle)).Length -1)
        {
            _worldStyle++;
        }
        else{
            _worldStyle = (WorldStyle)0;
        }

        _log.Trace(this, $"index stile {(int)_worldStyle}");

        GroundStyleInUse[0] = GroundStyleSprites[(int)_worldStyle, 0];
        GroundStyleInUse[1] = GroundStyleSprites[(int)_worldStyle, 1];
        GroundStyleInUse[2] = GroundStyleSprites[(int)_worldStyle, 2];

        foreach (var zolla in ElencoZolle)
        {
            SwapZollaStyle(zolla);
        }
    }

    private void SwapZollaStyle(GameObject zolla)
    {
        int step = zolla.GetComponent<GroundCollisionHandler>().Steps;
        zolla.GetComponent<SpriteRenderer>().sprite = GroundStyleInUse[step];
        _log.Trace(this, "stile zolla modificato");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
