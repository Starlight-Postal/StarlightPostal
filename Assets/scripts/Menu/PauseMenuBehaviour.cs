using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuBehaviour : NavigatableMenu {

    public bool inMenu = false;

    private float CONT_APSPECT_RATIO = 0.75f;

    private Button resumeButton;
    private Button resetButton;
    private Button quitMenuButton;
    private Button quitDesktopButton;
    private VisualElement container;
    private VisualElement rve;
    
    void Update() {        
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
        
        resumeButton.RegisterCallback<ClickEvent>(ev => { ResumeGame(); });
        resetButton.RegisterCallback<ClickEvent>(ev => { Reset(); });
        quitMenuButton.RegisterCallback<ClickEvent>(ev => { LoadMainMenu(); });
        quitDesktopButton.RegisterCallback<ClickEvent>(ev => { QuitGame(); });

        rve.visible = false;
        Rescale();
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
        GameObject.FindObjectsOfType<SaveFileManager>()[0].SaveSaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    private void QuitGame() {
        GameObject.FindObjectsOfType<SaveFileManager>()[0].SaveSaveData();
        Application.Quit();
    }

    void OnPauseGame() {
        inMenu = !inMenu;
    }

    private void Rescale() {
        Debug.Log("Resizing pause menu UI to new screen size");
        return; //nah bruh
        
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
        if ((float) Screen.width / (float) Screen.height >= 2)
        {
            contHeight /= 2;
            contWidth /= 2;
        }
#endif
#if UNITY_STANDALONE_OSX
        contHeight /= 2;
        contWidth /= 2;
        if ((float) Screen.width / (float) Screen.height >= 2)
        {
            contHeight /= 2;
            contWidth /= 2;
        }
#endif

        container.style.height = contHeight;
        container.style.width = contWidth;
    }

}
