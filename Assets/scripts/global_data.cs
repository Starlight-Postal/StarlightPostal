using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class global_data : MonoBehaviour
{
    public int coins;
    public int balloon;
    public int balloon0 = 1;
    public int balloon1 = 0;
    public int balloon2 = 0;
    public int balloon3 = 0;
    public int balloon4 = 0;
    public int balloon5 = 0;
    public int balloon6 = 0;
    public int balloon7 = 0;
    public int balloon8 = 0;
    public int balloon9 = 0;
    public int balloon10 = 0;
    public int stage = 1;

    // Start is called before the first frame update
    void Start()
    {
        coins = 1000;
        balloon = 0;
        DebugLogConsole.AddCommandInstance("data.coins", "Gets player coin count", "GetCoins", this);
        DebugLogConsole.AddCommandInstance("data.coins", "Sets player coin count", "SetCoins", this);
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
