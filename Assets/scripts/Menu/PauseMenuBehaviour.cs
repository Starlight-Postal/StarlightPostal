using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuBehaviour : MonoBehaviour {

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

        resumeButton.RegisterCallback<ClickEvent>(ev => { inMenu = false; });
        resetButton.RegisterCallback<ClickEvent>(ev => { Reset(); });
        quitMenuButton.RegisterCallback<ClickEvent>(ev => { LoadMainMenu(); });
        quitDesktopButton.RegisterCallback<ClickEvent>(ev => { Application.Quit(); });

        rve.visible = false;
    }

    private void Reset() {
        inMenu = false;
        if (Input.GetKey(KeyCode.LeftShift)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            CheckpointManager.Respawn();
        }
    }

    private void LoadMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    void OnPauseGame() {
        inMenu = !inMenu;
    }

}
