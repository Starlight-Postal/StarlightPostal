using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinSoundEffect : MonoBehaviour {

    public AudioMixerGroup mixer;

    private static CoinSoundEffect instance;

    private AudioSource source;
    private AudioClip[] clips;

    private List<AudioSource> toneSources = new List<AudioSource>();
    private AudioClip toneClip;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixer;

        clips = Resources.LoadAll<AudioClip>("audio/SFX/coin");

        toneClip = Resources.LoadAll<AudioClip>("audio/SFX/cointone")[0];

        instance = this;
    }

    private void Cleanup() {
        toneSources.RemoveAll(item => item == null);
        toneSources.ForEach(toneSource => {
            if (!toneSource.isPlaying) {
                Destroy(toneSource);
            }
        });
    }

    private void Play() {

        var clip = clips[Random.Range(0,clips.Length - 1)];
        source.PlayOneShot(clip, 1);

        var toneSource = gameObject.AddComponent<AudioSource>();
        toneSources.Add(toneSource);
        toneSource.outputAudioMixerGroup = mixer;
        toneSource.clip = toneClip;

        //                    C  D  E  G  A
        var notes = new int[]{0, 2, 4, 7, 9};
        var note = notes[Random.Range(0,notes.Length - 1)];

        //                      input clip is D
        toneSource.pitch = Mathf.Pow(2, (note-2)/12.0f);
        toneSource.Play();

        Cleanup();

    }

    public static void CoinCollect() {
        instance.Play();
    }

}
