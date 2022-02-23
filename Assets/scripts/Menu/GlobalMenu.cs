using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GlobalMenu : MonoBehaviour {

    public bool inMenu = false;
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            inMenu = !inMenu;
        }

        if (inMenu) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    void OnGUI() {
        if (inMenu) {
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 85, 200, 50), "Game Paused");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 50), "Return to game")) {
                inMenu = false;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Quit to menu")) {
                SceneManager.LoadScene("Main Menu");
            }
            
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 60, 200, 50), "Quit to desktop")) {
                Application.Quit();
            }
        }
    }

}
