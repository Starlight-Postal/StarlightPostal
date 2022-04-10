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
            x = (cam.aspect / ((float)16/9)) * initScale.x * 0.9f;
            y = (cam.aspect / ((float)16/9)) * initScale.y * 0.9f;
        } else {            
            y = ((cam.aspect / ((float)16/9)) * -1 + 2) * initScale.y * 0.9f;
            x = ((cam.aspect / ((float)16/9)) * -1 + 2) * initScale.x * 0.9f;
        }

        gameObject.transform.localScale = new Vector3 (x, y, initScale.z);
    }

}
