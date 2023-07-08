using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableItemBase
{
    internal GameHandler _gameHandler;
    internal Logger _log;
    internal PlayerController _player;
    public bool Initialized { get; private set; }
    public virtual string Name {get;set;}

    internal void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        Initialized = true;
        _log.Debug(this, "signori sono l'item");
    }

    public DisposableItemBase()
    {
        Start();
    }

    public virtual void Use() {}
}