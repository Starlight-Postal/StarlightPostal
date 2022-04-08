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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static global_data Inst() {
        return GameObject.Find("Coin Global Data").GetComponent<global_data>();
    }

    [ConsoleMethod("data.coins", "Gets current coin count")]
    public static void GetCoins() {
        Debug.Log("Coins: " + Inst().coins);
    }

    [ConsoleMethod("data.coins", "Gets current coin count")]
    public static void SetCoins(int newCoinValue) {
        Inst().coins = newCoinValue;
        GetCoins();
    }

}
