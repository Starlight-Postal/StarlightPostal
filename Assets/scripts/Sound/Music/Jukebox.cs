using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Jukebox : MonoBehaviour
{

    public AudioMixerGroup mixerGroup;
    public AudioClip groundLoop, airLoop;
    public double offset;

    private AudioSource groundSourceA, groundSourceB, airSourceA, airSourceB;
    private bool sourceA = true;
    private double lastScheduleStart;
    private player play;

    public const float FADE_RATE = 0.01f;

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

        play = GameObject.FindObjectOfType<player>();

        var scheduledTime = AudioSettings.dspTime + groundLoop.length - offset;
        groundSourceA.Play();
        airSourceA.Play();
        groundSourceB.PlayScheduled(scheduledTime);
        airSourceB.PlayScheduled(scheduledTime);
        lastScheduleStart = scheduledTime;
    }

    void FixedUpdate()
    {
        var scheduledTime = lastScheduleStart + groundLoop.length - offset;
        if (scheduledTime < AudioSettings.dspTime)
        {
            scheduledTime = AudioSettings.dspTime; // If the audio were to be scheduled in the past, schedule it now instead
        }
        if (sourceA)
        {
            if (!groundSourceA.isPlaying)
            {
                groundSourceA.PlayScheduled(scheduledTime);
                airSourceA.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = false;
            }
        } else
        {
            if (!groundSourceB.isPlaying)
            {
                groundSourceB.PlayScheduled(scheduledTime);
                airSourceB.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = true;
            }
        }

        if (play.inBalloon)
        {
            groundSourceA.volume += (0.0f - groundSourceA.volume) * FADE_RATE;
            groundSourceB.volume += (0.0f - groundSourceA.volume) * FADE_RATE;
            airSourceA.volume += (1.0f - airSourceA.volume) * FADE_RATE;
            airSourceB.volume += (1.0f - airSourceA.volume) * FADE_RATE;
        }
        else
        {
            groundSourceA.volume += (1.0f - groundSourceA.volume) * FADE_RATE;
            groundSourceB.volume += (1.0f - groundSourceA.volume) * FADE_RATE;
            airSourceA.volume += (0.0f - airSourceA.volume) * FADE_RATE;
            airSourceB.volume += (0.0f - airSourceA.volume) * FADE_RATE;
        }
    }

    void Update()
    {
        
    }
    
}
