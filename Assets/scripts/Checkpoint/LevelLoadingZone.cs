using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingZone : LandingPad {

    [SerializeField] string targetLevel;
    [HideInInspector] string prevScene;

    // Start is called before the first frame update
    void Start() {
        prevScene = SceneManager.GetActiveScene().name;
        scene = targetLevel;                
    }
    
    public override void Trigger() {
        reached = CheckpointManager.instance.UpdateCheckpoint(this);
        CheckpointManager.Respawn();
    }
}
