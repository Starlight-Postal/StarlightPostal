using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapManager : MonoBehaviour {

    private player playerScript;
    private PauseMenuBehaviour pause;
    private PlayerInput pi;

    private bool prevInBalloon;
    private bool prevInMenu;

    void Start() {
        playerScript = GameObject.Find("player").GetComponent<player>();
        pause = GameObject.Find("Pause Menu Document").GetComponent<PauseMenuBehaviour>();
        pi = GameObject.Find("player objects").GetComponent<PlayerInput>();
    }

    void Update() {
        if ((playerScript.inBalloon != prevInBalloon) || (pause.inMenu != prevInMenu)) {
            if (pause.inMenu) {
                pi.SwitchCurrentActionMap("UI");
            } else {
                pi.SwitchCurrentActionMap(playerScript.inBalloon ? "Balloon" : "Player");
            }
        }

        prevInBalloon = playerScript.inBalloon;
        prevInMenu = pause.inMenu;        
    }

}
