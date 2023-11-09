using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsDisplayBehaviour : MonoBehaviour
{

    private Label creditsLabel;

    private CreditAccumulator creditAcc;

    private bool bInsert = false;

    private void OnEnable()
    {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        creditsLabel = rve.Q<Label>("credit-counter-label");
        StartCoroutine(FlashInsertCorroutine());
    }

    private void Start()
    {
        creditAcc = FindObjectOfType<CreditAccumulator>();
        
        UpdateCreditLabel();
    }

    private void Update()
    {
        UpdateCreditLabel();
    }

    public void UpdateCreditLabel()
    {
        int c = creditAcc.GetCredits();
        
        if (bInsert && c <= 0)
        {
            creditsLabel.text = $"Insert Credits";
        }
        else
        {
            creditsLabel.text = $"Credits: {c}";
        }
    }

    private IEnumerator FlashInsertCorroutine()
    {
        while (true)
        {
            bInsert = !bInsert;
            yield return new WaitForSecondsRealtime(2.0f);
        }
    }
}
