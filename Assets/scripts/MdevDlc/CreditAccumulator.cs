using System;
using UnityEngine;

public class CreditAccumulator : MonoBehaviour
{
    public delegate void OnCreditInserted();
    public OnCreditInserted onCreditInserted;
    
    private int mCredits = 0;

    public void AddCredit(int credits)
    {
        if (credits < 0) return;
        
        mCredits += credits;
        onCreditInserted();
    }

    public bool TakeCredit(int credits)
    {
        if (credits < 0) return false;
        
        if (mCredits >= credits)
        {
            mCredits -= credits;
            return true;
        }

        return false;
    }

    public int GetCredits()
    {
        return mCredits;
    }
}