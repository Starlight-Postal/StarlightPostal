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
    public float musicVolume = 1f;
    public float polarity = 0;

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
        if (!groundSourceA.isPlaying && !groundSourceB.isPlaying)
        {
            scheduledTime = AudioSettings.dspTime + groundLoop.length - offset;
            groundSourceA.Stop();
            groundSourceB.Stop();
            airSourceA.Stop();
            airSourceB.Stop();
            groundSourceA.Play();
            airSourceA.Play();
            groundSourceB.PlayScheduled(scheduledTime);
            airSourceB.PlayScheduled(scheduledTime);
            lastScheduleStart = scheduledTime;
        }
        else
        {
            if (sourceA)
            {
                if (!groundSourceA.isPlaying)
                {
                    groundSourceA.PlayScheduled(scheduledTime);
                    airSourceA.PlayScheduled(scheduledTime);
                    lastScheduleStart = scheduledTime;
                    sourceA = false;
                }
            }
            else
            {
                if (!groundSourceB.isPlaying)
                {
                    groundSourceB.PlayScheduled(scheduledTime);
                    airSourceB.PlayScheduled(scheduledTime);
                    lastScheduleStart = scheduledTime;
                    sourceA = true;
                }
            }
        }

        if (play.inBalloon)
        {
            polarity += (1 - polarity) * FADE_RATE;
        }
        else
        {
            polarity += (0 - polarity) * FADE_RATE;
        }
        airSourceA.volume = musicVolume * polarity;
        airSourceB.volume = musicVolume * polarity;
        groundSourceA.volume = musicVolume * (1-polarity);
        groundSourceB.volume = musicVolume * (1-polarity);
    }
    
}
