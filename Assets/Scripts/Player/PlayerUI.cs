using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Text batteryLevel, loadLevel, coins, speed, seeds;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void SetStats()
    {
        // batteryLevel.text = $"Batt : {playerController.Battery.ToString()}/{playerController.MaxBattery}";
        // loadLevel.text = $"Load : {playerController.Load} / {playerController.Capacity}";
        // coins.text = $"$ {playerController.Coins}";
        // speed.text = $"Speed {playerController.MoveSpeed} / 10";
        // seeds.text = $"Seeds {playerController.Seeds} / {playerController.MaxSeeds}";
    }
}
