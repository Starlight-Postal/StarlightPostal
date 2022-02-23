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
    }

    void OnGUI() {
        if (inMenu) {
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
