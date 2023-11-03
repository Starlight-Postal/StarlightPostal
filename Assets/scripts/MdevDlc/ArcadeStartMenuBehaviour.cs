using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ArcadeStartMenuBehaviour : MonoBehaviour
{
    private Button newGameButton;
    private VisualElement insertCredit;

    private CreditAccumulator creditAccumulator;

    private const float FlashPeriod = 0.3f;

    private Coroutine textFlasher;

    public void OnAnyJoystickButtonDown()
    {
        StartNewGame();
    }

    private void OnEnable()
    {
        var rve = GetComponent<UIDocument>().rootVisualElement;
        
        newGameButton = rve.Q<Button>("new-game-button");
        insertCredit = rve.Q<VisualElement>("insert-credit-element");
        
        newGameButton.RegisterCallback<ClickEvent>(ev => { StartNewGame(); });
    }

    private void Start()
    {
        creditAccumulator = FindObjectOfType<CreditAccumulator>();
        creditAccumulator.onCreditInserted += UpdateLabelFlash;
        UpdateLabelFlash();
    }

    private void UpdateLabelFlash()
    {
        if (textFlasher != null)
            StopCoroutine(textFlasher);
        
        int credits = creditAccumulator.GetCredits();

        if (credits > 0)
        {
            insertCredit.visible = false;
            newGameButton.visible = true;
            
            textFlasher = StartCoroutine(FlashTextCorroutine());
        }
        else
        {
            newGameButton.visible = false;
            insertCredit.visible = true;
        }
    }

    private IEnumerator FlashTextCorroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(FlashPeriod);
            newGameButton.visible = !newGameButton.visible;
        }
    }

    private void StartNewGame()
    {
        if (!creditAccumulator) return;

        if (creditAccumulator.TakeCredit(1))
        {
            FindObjectOfType<SaveFileManager>().saveData.introScene = true;
            SceneManager.LoadScene("level 1");
        }
    }
}