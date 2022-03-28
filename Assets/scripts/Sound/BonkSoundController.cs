using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BonkSoundController : MonoBehaviour {

    private static float BONK_THRESHOLD = 3;

    public AudioMixerGroup mixer;

    private AudioSource source;
    private AudioClip[] clips;

    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;

        clips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk");
    }

    private void Play() {
        source.clip = clips[Random.Range(0,clips.Length - 1)];
        source.Play();
    }

    public void Bonk(float magnitude) {

        if (magnitude >= BONK_THRESHOLD) {
            Play();
        }
        
    }

}
