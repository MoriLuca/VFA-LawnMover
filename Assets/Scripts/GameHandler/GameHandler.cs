using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameHandler Istance;
    public Logger Logger;
    public Player Player;
    public GroundManager GroundManager;
    public UnitsManager UnitsManager;
    void Start()
    {
        Istance = this;
        Logger = gameObject.AddComponent<Logger>();
        Player = gameObject.AddComponent<Player>();
        GroundManager = gameObject.AddComponent<GroundManager>();
        UnitsManager = gameObject.AddComponent<UnitsManager>();
        Player.JustCreated += UnitsManager.JustCreatedHandler;
    }
}
