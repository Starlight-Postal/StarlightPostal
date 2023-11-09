using System;
using UnityEngine;

public class CreditInsertHandler : MonoBehaviour
{
    private CreditAccumulator creditAccumulator;
    
    private void Start()
    {
        creditAccumulator = FindObjectOfType<CreditAccumulator>();
    }

    public void OnCreditInsert()
    {
        OnCreditInsert(1);
    }

    public void OnCreditInsert(int credits)
    {
        if (!creditAccumulator)
            return;
        
        creditAccumulator.AddCredit(credits);
        
        try
        {
            CoinSoundEffect.CoinCollect();
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception while playing sound: {e}");
        }
    }
}
