using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BonkSoundController : MonoBehaviour {

    private static float BONK_HEAVY_THRESHOLD = 8;
    private static float BONK_MEDIUM_THRESHOLD = 5;
    private static float BONK_LIGHT_THRESHOLD = 0;

    public AudioMixerGroup mixer;

    private AudioSource source;
    private AudioClip[] heavyClips;
    private AudioClip[] mediumClips;
    private AudioClip[] lightClips;

    void Start() {

        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;

        heavyClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/heavy");
        mediumClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/medium");
        lightClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/light");

    }

    private void Play(AudioClip[] clipArr) {

        source.clip = clipArr[Random.Range(0,clipArr.Length - 1)];
        source.Play();

    }

    public void Bonk(float magnitude) {

        if (magnitude < BONK_LIGHT_THRESHOLD) {
            return;
        }

        if (magnitude >= BONK_HEAVY_THRESHOLD) {
            // Heavy bonkage
            Play(heavyClips);
        } else if (magnitude >= BONK_MEDIUM_THRESHOLD) {
            // Medium bonkage
            Play(mediumClips);
        } else if (magnitude >= BONK_LIGHT_THRESHOLD) {
            // Light bonkage
            Play(lightClips);
        }

    }

}
