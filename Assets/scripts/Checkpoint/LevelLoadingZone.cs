using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingZone : LandingPad {

    [SerializeField] string targetLevel;
    [HideInInspector] string prevScene;

    GameObject balloon;
    public float fadeOut = 0.025f;
    float f = 0;
    public cam_fade fade;

    bool go = false;
    Jukebox music;

    // Start is called before the first frame update
    void Start() {
        prevScene = SceneManager.GetActiveScene().name;
        scene = targetLevel;
        balloon = GameObject.Find("balloon");
        fade = GameObject.Find("fade").GetComponent<cam_fade>();
        go = false;
        f = 0;
        music = GameObject.Find("Jukebox").GetComponent<Jukebox>();
    }

    void FixedUpdate()
    {
        if (go)
        {
            balloon.SetActive(false);
            f += fadeOut;
            fade.fade = f;
            music.musicVolume = 1-f;
            if (f > 1)
            {
                loadNext();
            }
        }
    }
    
    public override void Trigger() {
        //Debug.Log("goodbye");
        go = true;
    }

    public void loadNext()
    {
        reached = CheckpointManager.instance.UpdateCheckpoint(this);
        CheckpointManager.Respawn();
    }
}
