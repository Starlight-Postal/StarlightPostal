using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFillScreenHorizontally : MonoBehaviour {

    void Start() {
        
        var scale = gameObject.transform.localScale;
        var cam = Camera.main;
        
        var x = (cam.aspect / ((float)16/9)) * scale.x;
        var y = (cam.aspect / ((float)16/9)) * scale.y;

        gameObject.transform.localScale = new Vector3 (x, y, scale.z);

    }

}
