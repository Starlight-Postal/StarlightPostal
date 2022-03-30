using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinSoundEffect : MonoBehaviour {

    public AudioMixerGroup mixer;

    private static CoinSoundEffect instance;

    private AudioSource source;
    private AudioClip[] clips;

    private AudioSource toneSource;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;

        clips = Resources.LoadAll<AudioClip>("audio/SFX/coin");

        toneSource = gameObject.AddComponent<AudioSource>();
        toneSource.clip = Resources.LoadAll<AudioClip>("audio/SFX/cointone")[0];
        toneSource.outputAudioMixerGroup = mixer;

        instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void Play() {
        var clip = clips[Random.Range(0,clips.Length - 1)];
        source.PlayOneShot(clip, 1);

        var notes = new int[]{0, 2, 4, 7, 9};
        var note = notes[Random.Range(0,notes.Length - 1)];

        toneSource.pitch = Mathf.Pow(2, (note-2)/12.0f);
        toneSource.Play();
    }

    public static void CoinCollect() {
        instance.Play();
    }

}
