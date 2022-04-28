using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using IngameDebugConsole;

public class PopHazard : MonoBehaviour {

    public bool noPop = false;

    public AudioMixerGroup mixer;
    
    private AudioSource source;
    private AudioClip[] clips;

    public camera cam;
    public cam_fade fade;

    bool dying = false;

    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        clips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/pop");

        cam = Camera.main.gameObject.GetComponent<camera>();
        fade = GameObject.Find("fade").GetComponent<cam_fade>();
        dying = false;

        // Register instance commands
        DebugLogConsole.AddCommandInstance("balloon.nopop", "Toggles the nopop cheat", "ToggleNopop", this);
    }

    void Update()
    {
        if(dying){
            if (fade.death == 2)
            {
                cam.snapToTarget();
                cam.follow = true;
                dying = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (noPop) { return; }
        if (other.gameObject.CompareTag("hazard")) {
            Debug.Log("Balloon has hit a hazard!");
            CheckpointManager.Respawn();
            //cam.snapToTarget();
            dying = true;
            cam.follow = false;
            fade.death = -1;
            Play();
        }
    }

    private void Play() {
        source.clip = clips[Random.Range(0,clips.Length - 1)];
        source.Play();
    }

    public void ToggleNopop() {
        noPop = !noPop;
        Debug.Log("NoPop cheat toggled to " + noPop);
    }

}
