using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMountInterractable : Interractable {

    private player p;

    void Start() {
        p = GameObject.Find("player").GetComponent<player>();
    }
    
    public override void OnPlayerInterract() {
        if (!p.inBalloon) {
            p.inBalloon = true;
        }
    }

}
