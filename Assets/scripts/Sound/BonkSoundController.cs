using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BonkSoundController : MonoBehaviour {

    private static float BONK_HEAVY_THRESHOLD = 8;
    private static float BONK_MEDIUM_THRESHOLD = 5;
    private static float BONK_LIGHT_THRESHOLD = 0;

    public AudioMixerGroup mixer;
    public AudioMixerGroup coinMixer;

    private AudioSource source;
    private AudioSource dropSource;
    private AudioClip[] heavyClips;
    private AudioClip[] mediumClips;
    private AudioClip[] lightClips;

    private AudioClip[] dropClips;

    void Start() {

        source = gameObject.AddComponent<AudioSource>();
        dropSource = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;
        dropSource.outputAudioMixerGroup = coinMixer;

        heavyClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/heavy");
        mediumClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/medium");
        lightClips = Resources.LoadAll<AudioClip>("audio/SFX/balloon/bonk/light");

        dropClips = Resources.LoadAll<AudioClip>("audio/SFX/coin/drop");

    }

    private void Play(AudioClip[] clipArr) {

        source.PlayOneShot(clipArr[Random.Range(0,clipArr.Length - 1)], 1);

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

    public void CoinsDropped(int coins) {

        dropSource.PlayOneShot(dropClips[Random.Range(0,dropClips.Length - 1)], 1);

    }

}
