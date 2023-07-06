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
    IEnumerator Start()
    {
        Istance = this;

        //Creazione logger con priorità
        Logger = gameObject.AddComponent<Logger>();
        yield return new WaitUntil(() => Logger.Initialized);

        //istanze
        GroundManager = gameObject.AddComponent<GroundManager>();
        UnitsManager = gameObject.AddComponent<UnitsManager>();

        //waiting necessari
        yield return new WaitUntil(() => GroundManager.Initialized);

        //recupero il player
        Player = GameObject.Find("Player").GetComponent<PlayerController>();

        //eventi
        Player.JustCreated += UnitsManager.JustCreatedHandler;


        //jobs
        GroundManager.GenerateGround();
    }
}
