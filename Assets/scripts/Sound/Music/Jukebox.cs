using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Jukebox : MonoBehaviour
{

    public AudioMixerGroup mixerGroup;
    public AudioClip groundLoop, airLoop;
    public double offsetSamples;

    private AudioSource groundSourceA, groundSourceB, airSourceA, airSourceB;
    private bool sourceA = true;
    private double lastScheduleStart;

    void Start()
    {
        groundSourceA = gameObject.AddComponent<AudioSource>();
        airSourceA = gameObject.AddComponent<AudioSource>();
        groundSourceB = gameObject.AddComponent<AudioSource>();
        airSourceB = gameObject.AddComponent<AudioSource>();

        groundSourceA.clip = groundLoop;
        groundSourceB.clip = groundLoop;
        airSourceA.clip = airLoop;
        airSourceB.clip = airLoop;

        groundSourceA.outputAudioMixerGroup = mixerGroup;
        groundSourceB.outputAudioMixerGroup = mixerGroup;
        airSourceA.outputAudioMixerGroup = mixerGroup;
        airSourceB.outputAudioMixerGroup = mixerGroup;

        var scheduledTime = AudioSettings.dspTime + groundLoop.length - offsetSamples;
        groundSourceA.Play();
        airSourceA.Play();
        groundSourceB.PlayScheduled(scheduledTime);
        airSourceB.PlayScheduled(scheduledTime);
        lastScheduleStart = scheduledTime;
    }

    void Update()
    {
        if (sourceA)
        {
            if (!groundSourceA.isPlaying)
            {
                var scheduledTime = lastScheduleStart + groundLoop.length - offsetSamples;
                groundSourceA.PlayScheduled(scheduledTime);
                airSourceA.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = false;
            }
        } else
        {
            if (!groundSourceB.isPlaying)
            {
                var scheduledTime = lastScheduleStart + groundLoop.length - offsetSamples;
                groundSourceB.PlayScheduled(scheduledTime);
                airSourceB.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = true;
            }

        }
    }
    
}
