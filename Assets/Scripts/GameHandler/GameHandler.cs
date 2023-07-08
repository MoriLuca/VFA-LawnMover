using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameHandler Istance;
    public Logger Logger;
    public PlayerController Player;
    public GroundManager GroundManager;
    public UnitsManager UnitsManager;
    public GameInfoUI GameInfoUI;

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
}
