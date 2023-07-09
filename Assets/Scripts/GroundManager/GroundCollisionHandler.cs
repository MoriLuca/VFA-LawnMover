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
    private List<DisposableItemBase> GoodItems = new List<DisposableItemBase>();
    private List<DisposableItemBase> BadItems = new List<DisposableItemBase>();
    private List<DisposableItemBase> DisasterItems = new List<DisposableItemBase>();

    void Start()
    {
        _gameHandler = GameObject.Find("GameManager").GetComponent<GameHandler>();
        _groundManager = _gameHandler.GroundManager;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        Initialized = true;

        //good items
        GoodItems.Add(new diSizeUP());
        GoodItems.Add(new diSpeedDown());
        GoodItems.Add(new diCameraZoomOut());
        
        //bad itms
        BadItems.Add(new diSizeDown());
        BadItems.Add(new diNos());
        BadItems.Add(new diSpeedUp());
        BadItems.Add(new diTeleport());
        BadItems.Add(new diSpeedDown());
        BadItems.Add(new diCameraZoomIn());
        
    }

    void Update()
    {
    }

    public void CollisionFromPlayerHandler()
    {
        _log.Trace(this, "Aiahhh");
        HandleGroundType();
        if(Steps == 1)FeelLucky();
    }

    public void ForceRefresh() => ChangeGroundType();

    private void HandleGroundType()
    {
        if(Steps > 2) Steps = 2;
        if(Steps == 2) return;
        Steps++;
        ChangeGroundType();        
    }

    private void ChangeGroundType()
    {
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
        if(_player.ItemInPoket is not null) return;
        var fortune = UnityEngine.Random.Range(0,1001);
        if(fortune>950) DropGoodItem();
        else if (fortune<70 && fortune > 20) DropBadItem();
        else if (fortune <= 1) DropASituation();

    }

    public void DropGoodItem()
    {
        var index = UnityEngine.Random.Range(0,GoodItems.Count);
        _log.Debug(this, $"dropped good item with index {index}");
        _player.ItemInPoket = GoodItems[index];
        _player.TriggerGameInfoUIRefresh();
    }
    public void DropBadItem()
    {
        _log.Debug(this, "dropped bad item");
        _player.ItemInPoket = BadItems[UnityEngine.Random.Range(0,BadItems.Count)];
        _player.TriggerGameInfoUIRefresh();
        
    }
    public void DropASituation()
    {
        _log.Debug(this, "we got a situation");
        //_player.RandomizeSize();
        _player.TriggerGameInfoUIRefresh();
    }
}