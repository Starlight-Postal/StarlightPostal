using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class NewMenuUi : MonoBehaviour {

    private Button continueButton;
    private Button newGameButton;
    private Button optionsButton;
    private Button quitButton;

    private void OnEnable() {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        continueButton = rve.Q<Button>("continue-button");
        newGameButton = rve.Q<Button>("new-game-button");
        optionsButton = rve.Q<Button>("options-button");
        quitButton = rve.Q<Button>("quit-button");

        continueButton.RegisterCallback<ClickEvent>(ev => { ContinueGame(); });
        newGameButton.RegisterCallback<ClickEvent>(ev => { StartNewGame(); });
        optionsButton.RegisterCallback<ClickEvent>(ev => { });
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
}
