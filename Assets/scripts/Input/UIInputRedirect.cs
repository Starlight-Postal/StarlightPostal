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

    void OnNavigateUp() {
        globals.BroadcastMessage("OnNavigateUp");
    }

    void OnNavigateDown() {
        globals.BroadcastMessage("OnNavigateDown");
    }

    void OnSelect() {
        globals.BroadcastMessage("OnSelect");
    }

    void OnDeviceLost() {
        globals.BroadcastMessage("OnControllerDisconnect");
    }

}
