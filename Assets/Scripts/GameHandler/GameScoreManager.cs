using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    private GameHandler _gameHandler;
    private Logger _log;
    private PlayerController _player;
    private GroundManager _groundManager;
    public bool Initialized {get; private set;}


    private int _totalStep;
    private int _sceneStep;
    private int _levelScore;
    
    void Start()
    {
        _gameHandler = GameHandler.Istance;
        _log = _gameHandler.Logger;
        _player = _gameHandler.Player;
        _groundManager = _gameHandler.GroundManager;
        Initialized = true;
    }


    public void _incrementLevelScore(int points)
    {
        //maybe lock, al momento non credo sia necessario, forse pero si per le mattonelle
        _levelScore += points;
    }

    public int CalculateLevelScore()
    {
        // da migliorare
        var zolle = _groundManager.ElencoZolle;
        var totaleZolle = zolle.Count;
        var zolleGood = zolle.Count(z=>z.GetComponent<GroundCollisionHandler>().Steps == 1);
        var richiestaLivello = 15;

        var completamento = (int)MapValue(0, totaleZolle, 0, 100, zolleGood);
        var completmentoRichiesto = (int)MapValue(0, richiestaLivello, 0, 100, completamento);
        _log.Debug(this, $"zolle: {totaleZolle}, bouone: {zolleGood}, percentage: {completamento}, rich livello :{completmentoRichiesto}");
        return completmentoRichiesto;
    }

    public double MapValue(double a0, double a1, double b0, double b1, double a)
    {
        return b0 + (b1 - b0) * ((a-a0)/(a1-a0));
    }

    


}