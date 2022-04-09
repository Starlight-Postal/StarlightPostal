using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class BalloonSoundManager : MonoBehaviour {

    public AudioMixerGroup mixer;
    public AudioClip[] clips = new AudioClip[3];
    public bool invertAxis = false;

    private AudioSource[] sources = new AudioSource[3];
    private bool sustaining = false;

    void Start() {
        for (int i = 0; i < 3; i++) {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = clips[i];
            sources[i].outputAudioMixerGroup = mixer;
        }
        sources[1].loop = true;        
    }

    void OnBurnVent(InputValue value) {
        var v = value.Get<float>();
        if (invertAxis) { v *= -1; }
        if (v > 0) {
            if (!sustaining) {
                sustaining = true;
                sources[0].Play();
                sources[1].PlayScheduled(AudioSettings.dspTime + clips[0].length);
            }

            for (int i = 0; i < 3; i++) {
                sources[i].volume = (v / 2) + 0.5f;
            }
        }

        if (sustaining && v == 0) {
            sustaining = false;
            sources[0].Stop();
            sources[1].Stop();
            sources[2].Play();
        }
    }

}
