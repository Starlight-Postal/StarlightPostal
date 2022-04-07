using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuBehaviour : NavigatableMenu {

    public bool inMenu = false;

    private Button resumeButton;
    private Button resetButton;
    private Button quitMenuButton;
    private Button quitDesktopButton;
    private VisualElement rve;
    
    void Update() {        
        Time.timeScale = inMenu ? 0 : 1;
        rve.visible = inMenu;
    }

    private void OnEnable() {
        rve = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = rve.Q<Button>("resume-button");
        resetButton = rve.Q<Button>("reset-button");
        quitMenuButton = rve.Q<Button>("quit-menu-button");
        quitDesktopButton = rve.Q<Button>("quit-desktop-button");

        resumeButton.RegisterCallback<ClickEvent>(ev => { ResumeGame(); });
        resetButton.RegisterCallback<ClickEvent>(ev => { Reset(); });
        quitMenuButton.RegisterCallback<ClickEvent>(ev => { LoadMainMenu(); });
        quitDesktopButton.RegisterCallback<ClickEvent>(ev => { QuitGame(); });

        rve.visible = false;
    }

    public override void ClickButton(int index) {
        switch (index) {
        case 0:
            ResumeGame();
            break;
        case 1:
            Reset();
            break;
        case 2:
            LoadMainMenu();
            break;
        case 3:
            QuitGame();
            break;
        default:
            break;
        }
    }

    private void ResumeGame() {
        inMenu = false;
    }

    private void Reset() {
        inMenu = false;
        /*if (Input.GetKey(KeyCode.LeftShift)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {*/
            CheckpointManager.Respawn();
        //}
    }

    private void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    private void QuitGame() {
        Application.Quit();
    }

    void OnPauseGame() {
        inMenu = !inMenu;
    }

}
