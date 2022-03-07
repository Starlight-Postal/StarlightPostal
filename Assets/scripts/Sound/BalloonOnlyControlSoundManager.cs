using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonOnlyControlSoundManager : ControlSoundManager {

    private balloon balloonScript;

    // Start is called before the first frame update
    public void Start() {
        base.Start();
        balloonScript = GetComponent<balloon>();
    }

    // Update is called once per frame
    public void Update() {
        if (balloonScript.player.inBalloon) {
            base.Update();
        }
    }
    
}
