using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameHandler Istance;
    public Logger Logger;
    public PlayerController Player;
    public GroundManager GroundManager;
    public GameScoreManager GameScoreManager;
    public UnitsManager UnitsManager;
    public GameInfoUI GameInfoUI;
    public GameObject VirtualCamera;

    IEnumerator Start()
    {
        Istance = this;

        //Creazione logger con priorità
        Logger = gameObject.AddComponent<Logger>();
        yield return new WaitUntil(() => Logger.Initialized);

        //istanze
        GroundManager = gameObject.AddComponent<GroundManager>();
        UnitsManager = gameObject.AddComponent<UnitsManager>();
        GameInfoUI = gameObject.AddComponent<GameInfoUI>();
        GameScoreManager = gameObject.AddComponent<GameScoreManager>();
        VirtualCamera = GameObject.FindWithTag("VirtualCamera");
        //recupero il player
        var playerGo = GameObject.Find("Player");
        Player = playerGo.AddComponent<PlayerController>();

        //waiting necessari
        yield return new WaitUntil(() => GroundManager.Initialized);

        //eventi
        Player.JustCreated += UnitsManager.JustCreatedHandler;
        Player.RefreshGameInfoUIEvent += GameInfoUI.RefreshGameInfoUIEventHandler;
        Player.ItemDiscardedEvent += GroundManager.ItemDiscardedEventHandler;
        //jobs
        GroundManager.GenerateGround();
    }

    public void ChangeCameraZoom(int steps) => 
        VirtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += steps;
}
