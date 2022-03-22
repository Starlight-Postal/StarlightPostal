using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopHazard : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("BalloonHazard")) {
            Debug.Log("Balloon has hit a hazard!");
            CheckpointManager.Respawn();
        }
    }

}
