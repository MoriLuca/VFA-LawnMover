using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GroundCollisionHandler : MonoBehaviour
{
    private Logger _log;
    private PlayerController _player;
    private GameHandler _gameHandler;
    private GroundManager _groundManager;
    public bool Initialized {get; private set;}
    public event EventHandler RefreshGameInfoUIEvent;
    public int Steps = 0;
    private int _life = 0;

    void Start()
    {
        _gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        _groundManager = _gameHandler.GroundManager;
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
        if(Steps > 2) Steps = 2;
        if(Steps == 2) return;
        Steps++;

        var spriteRender = gameObject.GetComponent<SpriteRenderer>();

        switch (Steps)
        {
            case 0: spriteRender.sprite = _groundManager.GroundStyleInUse[0]; break;
            case 1: spriteRender.sprite = _groundManager.GroundStyleInUse[1]; break;
            case 2: spriteRender.sprite = _groundManager.GroundStyleInUse[2]; break;
            // defautl magari creare un default sprite per eventuali errori
        }
    }

    private void FeelLucky()
    {
        var fortune = UnityEngine.Random.Range(0,1001);
        if(fortune>950) DropGoodItem();
        else if (fortune<70 && fortune > 20) DropBadItem();
        // else if (fortune <= 1) DropASituation();

    }

    public void DropGoodItem()
    {
        _log.Debug(this, "dropped good item");
        _player.ItemInPoket = new diSizeUP();
        _player.TriggerGameInfoUIRefresh();
    }
    public void DropBadItem()
    {
        _log.Debug(this, "dropped bad item");
        _player.ItemInPoket = new diSizeDown();
        _player.TriggerGameInfoUIRefresh();
        
    }
    public void DropASituation()
    {
        _log.Debug(this, "we got a situation");
        //_player.RandomizeSize();
        _player.TriggerGameInfoUIRefresh();
    }
}