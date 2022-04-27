using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boombox : MonoBehaviour
{
    public AudioSource loop;
    public GameObject package;
    float vol;
    // Start is called before the first frame update
    void Start()
    {
        vol = loop.volume;
    }

    // Update is called once per frame
    void Update()
    {
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
