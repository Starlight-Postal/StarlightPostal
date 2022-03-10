using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundEffect : MonoBehaviour {

    private static CoinSoundEffect instance;

    private AudioSource source;
    private AudioClip[] clips;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.AddComponent<AudioSource>();

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
