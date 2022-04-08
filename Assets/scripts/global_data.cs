using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class global_data : MonoBehaviour
{
    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;

        // Register instance commands
        DebugLogConsole.AddCommandInstance("data.coins", "Gets current coin count", "GetCoins", this);
        DebugLogConsole.AddCommandInstance("data.coins", "Sets current coin count", "SetCoins", this);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetCoins() {
        Debug.Log("Coins: " + coins);
    }

    public void SetCoins(int newCoinValue) {
        coins = newCoinValue;
        GetCoins();
    }

}
