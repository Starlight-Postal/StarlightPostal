using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IngameDebugConsole;

public class CheckpointManager : MonoBehaviour {

    public static CheckpointManager instance = null;

    SaveFileManager savemgr;

    private bool needsMoveOnceSceneIsLoaded = false;

    void Start() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoad;
        } else {
            Destroy(this.gameObject);
        }

        savemgr = GameObject.FindObjectsOfType<SaveFileManager>()[0];
    }

    void Update() {
        
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        if (needsMoveOnceSceneIsLoaded && savemgr.saveData.checkpointScene == scene.name) {
            needsMoveOnceSceneIsLoaded = false;
            MoveBalloon();
            GameObject.Find("Main Camera").GetComponent<camera>().Teleport();
        }
    }

    public bool UpdateCheckpoint(LandingPad pad) {
        //Debug.Log("event has bubbled up from pad #" + pad.padId);

        if (!pad.overrides) {
            if (pad.scene == savemgr.saveData.checkpointScene) {
                if (pad.padId <= savemgr.saveData.checkpointId) {
                    // Only continue if this pad has a higher id, not in the same scene, or is marked to override
                    Debug.LogWarning("Refusing to set new checkpoint! Lower ID than previous for non-overriding checkpoint.");
                    return false;
                }
            }
        }        

        savemgr.saveData.checkpointId = pad.padId;
        savemgr.saveData.checkpointScene = pad.scene;
        return true;
    }

    public Vector3 GetRespawnPoint() {
        var pads = GameObject.FindObjectsOfType<LandingPad>();
        LandingPad spawnPad = null;

        foreach (var pad in pads) {
            if (pad.padId == savemgr.saveData.checkpointId) {
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
        savemgr.saveData.checkpointScene = SceneManager.GetActiveScene().name;
    }

    [ConsoleMethod("respawn", "teleports balloon to last checkpoint")]
    public static void Respawn() {
        Debug.Log("Teleporting...");
        if (SceneManager.GetActiveScene().name != instance.savemgr.saveData.checkpointScene) {
            Debug.LogWarning("Checkpoint is not from this scene!");
            SceneManager.LoadScene(instance.savemgr.saveData.checkpointScene);
            instance.needsMoveOnceSceneIsLoaded = true;
            return;
        }

        instance.MoveBalloon();
    }

    [ConsoleMethod("checkpoint.id", "set new checkpoint id")]
    public static void SetNewCheckpointId(int id) {
        instance.savemgr.saveData.checkpointId = id;
        Debug.Log("Checkpoint ID set.");
    }

    [ConsoleMethod("checkpoint.id", "get current checkpoint id")]
    public static int GetCheckpointId() {
        return instance.savemgr.saveData.checkpointId;
    }

    [ConsoleMethod("checkpoint.scene", "set new checkpoint scene")]
    public static void SetNewCheckpointScene(string scene) {
        instance.savemgr.saveData.checkpointScene = scene;
        Debug.Log("Checkpoint scene set.");
    }

    [ConsoleMethod("checkpoint.scene", "get current checkpoint scene")]
    public static string GetCheckpointScene() {
        return instance.savemgr.saveData.checkpointScene;
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
