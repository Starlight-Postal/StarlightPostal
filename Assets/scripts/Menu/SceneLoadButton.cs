using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoadButton : MonoBehaviour {

    public string firstLevelScene;

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Play Game")) {
            Debug.Log("Button Clicked");
            SceneManager.LoadScene(firstLevelScene);
        }
    }
}
