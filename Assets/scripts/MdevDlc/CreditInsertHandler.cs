using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
