using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinSoundEffect : MonoBehaviour {

    public AudioMixerGroup mixer;

    private static CoinSoundEffect instance;

    private AudioSource source;
    private AudioClip[] clips;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;

        clips = Resources.LoadAll<AudioClip>("audio/SFX/coin");
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Play() {
        source.clip = clips[Random.Range(0,clips.Length - 1)];
        source.Play();
    }

    public static void CoinCollect() {
        instance.Play();
    }

}
