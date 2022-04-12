using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFillScreenHorizontally : MonoBehaviour {

    private Vector2 lastScreenSize;
    private Vector3 initScale;
    public float buffer = 1;

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
            x = (cam.aspect / ((float)16/9)) * initScale.x * buffer;
            y = (cam.aspect / ((float)16/9)) * initScale.y * buffer;
        } else {            
            y = ((cam.aspect / ((float)16/9)) * -1 + 2) * initScale.y * buffer;
            x = ((cam.aspect / ((float)16/9)) * -1 + 2) * initScale.x * buffer;
        }

        gameObject.transform.localScale = new Vector3 (x, y, initScale.z);
    }

}
