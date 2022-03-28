using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PopHazard : MonoBehaviour {

    public AudioMixerGroup mixer;
    
    private AudioSource source;
    private AudioClip[] clips;

    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        clips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/pop");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("hazard")) {
            Debug.Log("Balloon has hit a hazard!");
            CheckpointManager.Respawn();
            Play();
        }
    }

    private void Play() {
        source.clip = clips[Random.Range(0,clips.Length - 1)];
        source.Play();
    }

}
