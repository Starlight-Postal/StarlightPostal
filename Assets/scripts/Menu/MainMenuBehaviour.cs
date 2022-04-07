using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuBehaviour : NavigatableMenu {

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
}
