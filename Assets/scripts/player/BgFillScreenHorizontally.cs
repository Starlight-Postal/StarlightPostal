using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFillScreenHorizontally : MonoBehaviour {

    private Vector2 lastScreenSize;
    private Vector3 initScale;

    void Start() {
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        initScale = gameObject.transform.localScale;
        Resize();
    }

    void Update() {
        var screenSize = new Vector2(Screen.width, Screen.height);
        if (lastScreenSize != screenSize) {
            Resize();
            lastScreenSize = screenSize;
        }
    }

    private void Resize() {
        var cam = Camera.main;
        
        float x, y;
        if (cam.aspect >= (float)16/9) {
            x = (cam.aspect / ((float)16/9)) * initScale.x;
            y = (cam.aspect / ((float)16/9)) * initScale.y;
        } else {            
            y = ((1 / cam.aspect) * ((float)16/9)) * initScale.y;
            x = ((1 / cam.aspect) * ((float)16/9)) * initScale.x;
        }

        gameObject.transform.localScale = new Vector3 (x, y, initScale.z);
    }

}
