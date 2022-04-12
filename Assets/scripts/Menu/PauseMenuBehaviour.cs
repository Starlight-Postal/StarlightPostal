using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuBehaviour : MonoBehaviour {

    public bool inMenu = false;

    private float CONT_APSPECT_RATIO = 0.75f;

    private Button resumeButton;
    private Button resetButton;
    private Button quitMenuButton;
    private Button quitDesktopButton;
    private VisualElement container;
    private VisualElement rve;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            inMenu = !inMenu;
        }
        
        Time.timeScale = inMenu ? 0 : 1;
        rve.visible = inMenu;
    }

    private void OnEnable() {
        rve = GetComponent<UIDocument>().rootVisualElement;

        container = rve.Q<VisualElement>("pause-menu-container");

        resumeButton = rve.Q<Button>("resume-button");
        resetButton = rve.Q<Button>("reset-button");
        quitMenuButton = rve.Q<Button>("quit-menu-button");
        quitDesktopButton = rve.Q<Button>("quit-desktop-button");

        rve.RegisterCallback<GeometryChangedEvent>(ev => { Rescale(); });

        resumeButton.RegisterCallback<ClickEvent>(ev => { inMenu = false; });
        resetButton.RegisterCallback<ClickEvent>(ev => { Reset(); });
        quitMenuButton.RegisterCallback<ClickEvent>(ev => { LoadMainMenu(); });
        quitDesktopButton.RegisterCallback<ClickEvent>(ev => { Application.Quit(); });

        rve.visible = false;
        Rescale();
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
