using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableItem : MonoBehaviour
{
    private GameHandler _gameHandler;
    private Logger _log;
    private PlayerController _player;
    public bool Initialized {get; private set;}

    void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        Initialized = true;
    }
    void Update() {
        
    }
    public void Use()
    {
        
    }
}