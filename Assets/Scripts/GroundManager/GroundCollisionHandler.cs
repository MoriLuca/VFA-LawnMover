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
        if(Steps++ >= 2) return;
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