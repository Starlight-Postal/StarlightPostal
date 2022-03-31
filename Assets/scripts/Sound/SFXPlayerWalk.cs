using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerWalk : MonoBehaviour {

    public AudioMixerGroup mixer;

    private player p;

    private List<AudioSource> sources = new List<AudioSource>();
    private AudioClip[] clips;

    private int lastFrame;

    void Start() {

        p = GameObject.Find("player").GetComponent<player>();

        clips = Resources.LoadAll<AudioClip>("audio/SFX/player/walk");
        
    }

    private void Cleanup() {
        sources.RemoveAll(item => item == null);
        sources.ForEach(source => {
            if (!source.isPlaying) {
                Destroy(source);
            }
        });
    }

    void PlayStep() {

        var clip = clips[Random.Range(0,clips.Length - 1)];

        var source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
        source.outputAudioMixerGroup = mixer;
        source.clip = clip;

        source.pitch = Random.Range(0.8f, 1.2f);

        source.Play();

        Cleanup();
        
    }

    void FixedUpdate() {

        if (p.inBalloon) { return; }

        if (p.aniMode == "walk") {
            var frame = (int) p.aniFrame;
            if (frame == 17 || frame == 8) {
                if (frame != lastFrame) {
                    lastFrame = frame;
                    PlayStep();
                }
            }
        } else {
            lastFrame = -1;
        }

    }

}
