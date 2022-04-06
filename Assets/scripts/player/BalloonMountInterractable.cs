using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalloonMountInterractable : Interractable {

    private player p;

    void Start() {
        p = GameObject.Find("player").GetComponent<player>();
    }
    
    public override void OnPlayerInterract() {
        if (!p.inBalloon) {
            GameObject.Find("player objects").GetComponent<PlayerInput>().SwitchCurrentActionMap("Balloon");
            p.inBalloon = true;
        }
    }

}
