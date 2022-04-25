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
    public float fade = 0.025f;

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
        if (sourceA)
        {
            if (!groundSourceA.isPlaying)
            {
                var scheduledTime = lastScheduleStart + groundLoop.length - offset;
                groundSourceA.PlayScheduled(scheduledTime);
                airSourceA.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = false;
            }
        } else
        {
            if (!groundSourceB.isPlaying)
            {
                var scheduledTime = lastScheduleStart + groundLoop.length - offset;
                groundSourceB.PlayScheduled(scheduledTime);
                airSourceB.PlayScheduled(scheduledTime);
                lastScheduleStart = scheduledTime;
                sourceA = true;
            }
        }
    }

    void Update()
    {
        if (play.inBalloon)
        {
            groundSourceA.volume += (0.0f- groundSourceA.volume)*fade;
            groundSourceB.volume += (0.0f- groundSourceB.volume)*fade;
            airSourceA.volume += (1.0f- airSourceA.volume) *fade;
            airSourceB.volume += (1.0f- airSourceB.volume) *fade;
        } else
        {
            groundSourceA.volume += (1.0f - groundSourceA.volume) * fade;
            groundSourceB.volume += (1.0f - groundSourceB.volume)*fade;
            airSourceA.volume += (0.0f - airSourceA.volume) * fade;
            airSourceB.volume += (0.0f - airSourceB.volume) * fade;
        }
    }
    
}
