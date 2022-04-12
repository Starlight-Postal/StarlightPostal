using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuBehaviour : MonoBehaviour {

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
        optionsButton.RegisterCallback<ClickEvent>(ev => { Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ"); });
        quitButton.RegisterCallback<ClickEvent>(ev => { QuitGame(); });
    }

    private void ContinueGame() {
        StartNewGame();
    }

    private void StartNewGame() {
        SceneManager.LoadScene("level 1");
    }

    private void QuitGame() {
        Application.Quit();
    }

    private void Rescale() {        
        float contWidth;
        float contHeight;
        
        if (Screen.width <= Screen.height) {
            contWidth = Screen.width * 0.8f;
            contHeight = contWidth * (1f / CONT_APSPECT_RATIO);
        } else {
            contHeight = Screen.height * 0.66f;
            contWidth = contHeight * CONT_APSPECT_RATIO;
        }
            
        container.style.height = contHeight;
        container.style.width = contWidth;
    }
}
