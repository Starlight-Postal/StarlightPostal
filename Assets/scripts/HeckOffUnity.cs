using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeckOffUnity : MonoBehaviour {

    private static bool heckedOff = false;

    // Start is called before the first frame update
    void Start() {
        if (heckedOff) {
            Destroy(this.gameObject);
        } else {
            heckedOff = true;
            DontDestroyOnLoad(this.gameObject);
        }   
    }
}
