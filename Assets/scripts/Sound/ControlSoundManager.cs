using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ControlSoundManager : MonoBehaviour {

    public AudioMixerGroup mixer;

    public AudioClip[] clips = new AudioClip[3];
    public KeyCode[] keys = new KeyCode[1];

    private bool keyDown = false;
    private AudioSource[] sources = new AudioSource[3];

    // Start is called before the first frame update
    public void Start() {
        for (int i = 0; i < 3; i++) {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = clips[i];
            sources[i].outputAudioMixerGroup = mixer;
        }
        sources[1].loop = true;
    }

    // Update is called once per frame
    public void Update() {
        bool prevKeyDown = keyDown;
        keyDown = false;
        for (int i = 0; i < keys.Length; i++) {
            if (Input.GetKey(keys[i])) {
                keyDown = true;
            }
        }
        
        if (prevKeyDown != keyDown) {
            if (keyDown) {
                sources[0].Play();
                sources[1].PlayScheduled(AudioSettings.dspTime + clips[0].length);
            } else {
                sources[0].Stop();
                sources[1].Stop();
                sources[2].Play();
            }
        }
    }

}
