using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boombox : MonoBehaviour
{
    public AudioSource loop;
    public GameObject package;
    float vol;
    float delay = 2.25f;
    bool play = false;
    float stime;
    // Start is called before the first frame update
    void Start()
    {
        vol = loop.volume;
        play = false;
        stime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
        {
            if (Time.time-stime >= delay)
            {
                play = true;
                loop.Play(0);
            }
        }
        if (package.active)
        {
            loop.volume = vol;
        }
        else
        {
            loop.volume = 0;
        }
    }
}
