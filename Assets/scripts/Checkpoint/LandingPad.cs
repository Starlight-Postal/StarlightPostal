using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingPad : MonoBehaviour {

    [SerializeField]
    public int padId = 0;

    [SerializeField]
    public bool overrides = false;

    [SerializeField]
    public bool reached = false;

    [HideInInspector]
    public string scene;
    
    void Start() {
        scene = SceneManager.GetActiveScene().name;        
    }

    void Update() {
        var sprite = GetComponentInChildren<SpriteRenderer>();
        if (reached) {
            sprite.color = new Color(1,0.9f,0.9f);
        } else {
            sprite.color = new Color(0,0,0);
        }
    }

    public void Trigger() {
        reached = true;
        CheckpointManager.instance.UpdateCheckpoint(this);
    }

}
