using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsDisplayBehaviour : MonoBehaviour
{

    private Label creditsLabel;

    private CreditAccumulator creditAcc;

    private void OnEnable()
    {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        creditsLabel = rve.Q<Label>("credit-counter-label");
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
        creditsLabel.text = $"Credits: {c}";
    }
}
