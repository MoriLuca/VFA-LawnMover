using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableItemBase : MonoBehaviour
{
    internal GameHandler _gameHandler;
    internal Logger _log;
    internal PlayerController _player;
    public bool Initialized { get; private set; }
    public string Name {get;set;}

    internal void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        Initialized = true;
    }
    public virtual void Use() {}
}