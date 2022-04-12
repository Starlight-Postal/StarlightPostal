using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IngameDebugConsole;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager instance = null;

    private int lastCheckpointId;
    private string lastCheckpointScene;

    private bool needsMoveOnceSceneIsLoaded = false;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoad;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Update() {
        
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        if (needsMoveOnceSceneIsLoaded) {
            needsMoveOnceSceneIsLoaded = false;
            MoveBalloon();
            GameObject.Find("Main Camera").GetComponent<camera>().Teleport();
        }
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
            if (pad.padId == lastCheckpointId) {
                spawnPad = pad;
                break;
            }
        }

        var pos = spawnPad.transform.Find("Spawn Point").transform.position;
        return pos;
    }

    private void MoveBalloon() {
        var balloon = GameObject.Find("balloon");
        var center = GameObject.Find("Center");

        var respawnPosition = GetRespawnPoint();
        respawnPosition.z = balloon.transform.position.z;

        var prevCenterPos = center.transform.position;
        var move = respawnPosition - center.transform.position;

        balloon.transform.position += move;

        balloon.GetComponent<balloon>().th = respawnPosition.y;
        balloon.GetComponent<balloon>().anchored = false;
        var pl = GameObject.Find("player").GetComponent<player>();
        pl.inBalloon = true;
        pl.free = true;
        GameObject.Find("Main Camera").GetComponent<camera>().follow = true;
        lastCheckpointScene = SceneManager.GetActiveScene().name;
    }

    [ConsoleMethod("respawn", "teleports balloon to last checkpoint")]
    public static void Respawn() {
        Debug.Log("Teleporting...");
        if (SceneManager.GetActiveScene().name != instance.lastCheckpointScene) {
            Debug.LogWarning("Checkpoint is not from this scene!");
            SceneManager.LoadScene(instance.lastCheckpointScene);
            instance.needsMoveOnceSceneIsLoaded = true;
            return;
        }

        instance.MoveBalloon();
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

    [ConsoleMethod("goto", "Quickly teleport to a checkpoint")]
    public static void GotoCheckpoint(int id) {
        SetNewCheckpointId(id);
        SetNewCheckpointScene(SceneManager.GetActiveScene().name);
        Respawn();
    }

    [ConsoleMethod("goto", "Quickly teleport to a checkpoint")]
    public static void GotoCheckpoint(int id, string scene) {
        SetNewCheckpointId(id);
        SetNewCheckpointScene(scene);
        Respawn();
    }
}
