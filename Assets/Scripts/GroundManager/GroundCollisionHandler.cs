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
        var fortune = Random.Range(0,1001);
        if(fortune>950) DropGoodItem();
        else if (fortune<70 && fortune > 20) DropBadItem();
        // else if (fortune <= 1) DropASituation();

    }

    public void DropGoodItem()
    {
        _log.Trace(this, "dropped good item");
        var items = _player.gameObject.GetComponents<DisposableItemBase>();
        if(items != null && items.Length > 0) foreach (var i in items) Destroy(i);
        _player.gameObject.AddComponent<diSizeUP>();
    }
    public void DropBadItem()
    {
        _log.Trace(this, "dropped bad item");
        var items = _player.gameObject.GetComponents<DisposableItemBase>();
        if(items != null && items.Length > 0) foreach (var i in items) Destroy(i);
        _player.gameObject.AddComponent<diSizeDown>();
        
    }
    public void DropASituation()
    {
        _log.Trace(this, "we got a situation");
        //_player.RandomizeSize();
    }
}