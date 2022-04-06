using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputRedirect : MonoBehaviour {

    private GameObject globals;

    void Start() {
        globals = GameObject.Find("Globals");
    }

    void OnPauseGame() {
        globals.BroadcastMessage("OnPauseGame");
    }

}
