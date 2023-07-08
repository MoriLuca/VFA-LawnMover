using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInfoUI : MonoBehaviour
{
    private GameHandler _gameHandler;
    private Logger _log;
    private PlayerController _player;
    public bool Initialized {get; private set;}
    private GameObject _infoContainerObject;
    //cointainer UI elements
    private GameObject _info;
    private GameObject _capsule;
    private GameObject _speed;
    
    public void RefreshGameInfoUIEventHandler(object sender, object args)
    {
        RefreshUI();
    }


    void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        _infoContainerObject = GameObject.FindGameObjectWithTag("GameInfo");
        _info = _infoContainerObject.transform.GetChild(0).gameObject;
        _capsule = _infoContainerObject.transform.GetChild(1).gameObject;
        _speed = _infoContainerObject.transform.GetChild(2).gameObject;
        Initialized = true;
        RefreshUI();
    }

    private void RefreshUI() 
    {
        _capsule.SetActive(_player.HasCapsuleAvailable());
        _speed.GetComponent<TextMeshProUGUI>().text = $"Speed {_player.MoveSpeed}";
    }
}