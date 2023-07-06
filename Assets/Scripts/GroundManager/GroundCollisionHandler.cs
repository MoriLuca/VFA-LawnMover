using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroundCollisionHandler : MonoBehaviour
{
    private Logger _log;
    private PlayerController _player;
    private GameHandler _gameHandler;
    public bool Initialized {get; private set;}
    public int Steps = 0;
    private int _life = 0;

    void Start()
    {
        _gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        Initialized = true;
    }

    void Update()
    {
    }

    public void CollisionFromPlayerHandler()
    {
        _log.Trace(this, "Aiahhh");
        FeelLucky();
        HandleGroundType();
    }

    private void HandleGroundType()
    {
        if(Steps == 2) return;
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        if(++Steps > 2) Steps = 2;
        switch (Steps)
        {
            case 0: spriteRender.color = new Color32(170, 200, 167, 255); break;
            case 1: spriteRender.color = new Color32(233,255,194, 255) ; break;
            case 2: spriteRender.color = new Color32(195, 129, 84, 255); break;
            default: spriteRender.color = Color.white; break;
        }
    }

    private void FeelLucky()
    {
        var fortune = Random.Range(0,101);
        if(fortune>95) DropGoodItem();
        else if (fortune<6 && fortune > 2) DropBadItem();
        else if (fortune <= 1) DropASituation();

    }

    public void DropGoodItem()
    {
        _log.Debug(this, "dropped good item");
        _player.SizeUp();
    }
    public void DropBadItem()
    {
        _log.Debug(this, "dropped bad item");
        _player.SizeDown();
    }
    public void DropASituation()
    {
        _log.Debug(this, "we got a situation");
        _player.RandomizeSize();
    }
}