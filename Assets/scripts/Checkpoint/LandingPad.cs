using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingPad : MonoBehaviour {

    [SerializeField]
    public int padId = 0;

    [SerializeField]
    public bool overrides = false;

    [HideInInspector]
    public string scene;
    
    void Start() {
        scene = SceneManager.GetActiveScene().name;        
    }

    public void Trigger() {
        CheckpointManager.instance.UpdateCheckpoint(this);
    }

}
