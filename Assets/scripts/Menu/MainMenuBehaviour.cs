using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuBehaviour : NavigatableMenu {

    private float CONT_APSPECT_RATIO = 0.75f;

    private Button continueButton;
    private Button newGameButton;
    private Button optionsButton;
    private Button quitButton;
    private VisualElement container;

    private void OnEnable() {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        container = rve.Q<VisualElement>("main-menu-container");

        continueButton = rve.Q<Button>("continue-button");
        newGameButton = rve.Q<Button>("new-game-button");
        optionsButton = rve.Q<Button>("options-button");
        quitButton = rve.Q<Button>("quit-button");

        
        rve.RegisterCallback<GeometryChangedEvent>(ev => { Rescale(); });

        continueButton.RegisterCallback<ClickEvent>(ev => { ContinueGame(); });
        newGameButton.RegisterCallback<ClickEvent>(ev => { StartNewGame(); });
        optionsButton.RegisterCallback<ClickEvent>(ev => { OpenOptions(); });
        quitButton.RegisterCallback<ClickEvent>(ev => { QuitGame(); });
    }

    public override void ClickButton(int index) {
        switch (index) {
        case 0:
            ContinueGame();
            break;
        case 1:
            StartNewGame();
            break;
        case 2:
            OpenOptions();
            break;
        case 3:
            QuitGame();
            break;
        default:
            break;
        }
    }

    private void ContinueGame() {
        StartNewGame();
    }

    private void StartNewGame() {
        SceneManager.LoadScene("level 1");
    }

    private void OpenOptions() {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }

    private void QuitGame() {
        Application.Quit();
    }

    private void Rescale() {
        Debug.Log("Resizing main menu UI to new screen size");
        
        float contWidth;
        float contHeight;
        
        if (Screen.width <= Screen.height) {
            contWidth = Screen.width * 0.8f;
            contHeight = contWidth * (1f / CONT_APSPECT_RATIO);
        } else {
            contHeight = Screen.height * 0.66f;
            contWidth = contHeight * CONT_APSPECT_RATIO;
        }

        #if PLATFORM_ANDROID
        contHeight /= 2;
        contWidth /= 2;
        #endif
            
        container.style.height = contHeight;
        container.style.width = contWidth;
    }
}
