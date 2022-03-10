using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

    private LandingPad pad;

    void Start() {
        pad = this.gameObject.transform.parent.gameObject.GetComponent<LandingPad>();
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.parent.gameObject.name == "balloon") {
            //Debug.Log("The balloon has triggered a checkpoint");
            pad.Trigger();
        }
    }
}
