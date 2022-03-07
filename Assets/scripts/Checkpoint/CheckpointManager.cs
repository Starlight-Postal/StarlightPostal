using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IngameDebugConsole;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager instance;

    private int lastCheckpointId;
    private string lastCheckpointScene;

    void Start() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        
    }

    public bool UpdateCheckpoint(LandingPad pad) {
        //Debug.Log("event has bubbled up from pad #" + pad.padId);

        if (!pad.overrides) {
            if (pad.scene == lastCheckpointScene) {
                if (pad.padId <= lastCheckpointId) {
                    // Only continue if this pad has a higher id, not in the same scene, or is marked to override
                    Debug.LogWarning("Refusing to set new checkpoint! Lower ID than previous for non-overriding checkpoint.");
                    return false;
                }
            }
        }        

        lastCheckpointId = pad.padId;
        lastCheckpointScene = pad.scene;
        return true;
    }

    public Vector3 GetRespawnPoint() {
        var pads = GameObject.FindObjectsOfType<LandingPad>();
        LandingPad spawnPad = null;

        foreach (var pad in pads) {
            if (pad.padId == lastCheckpointId && pad.scene == lastCheckpointScene) {
                spawnPad = pad;
                break;
            }
        }

        var pos = spawnPad.transform.position + new Vector3(0, 3, 0);
        return pos;
    }

    [ConsoleMethod("respawn", "teleports balloon to last checkpoint")]
    public static void Respawn() {
        Debug.Log("Teleporting...");
        if (SceneManager.GetActiveScene().name != instance.lastCheckpointScene) {
            Debug.LogWarning("Checkpoint is not from this scene!");
            SceneManager.LoadScene(instance.lastCheckpointScene);
        }

        var balloon = GameObject.Find("balloon");
        var center = GameObject.Find("Center");

        var respawnPosition = instance.GetRespawnPoint();
        respawnPosition.z = balloon.transform.position.z;

        var prevCenterPos = center.transform.position;
        var move = respawnPosition - center.transform.position;

        balloon.transform.position += move;

        balloon.GetComponent<balloon>().th = balloon.transform.position.y;
    }

    [ConsoleMethod("checkpoint.id", "set new checkpoint id")]
    public static void SetNewCheckpointId(int id) {
        instance.lastCheckpointId = id;
        Debug.Log("Checkpoint ID set.");
    }

    [ConsoleMethod("checkpoint.id", "get current checkpoint id")]
    public static int GetCheckpointId() {
        return instance.lastCheckpointId;
    }

    [ConsoleMethod("checkpoint.scene", "set new checkpoint scene")]
    public static void SetNewCheckpointScene(string scene) {
        instance.lastCheckpointScene = scene;
        Debug.Log("Checkpoint scene set.");
    }

    [ConsoleMethod("checkpoint.scene", "get current checkpoint scene")]
    public static string GetCheckpointScene() {
        return instance.lastCheckpointScene;
    }    

}
