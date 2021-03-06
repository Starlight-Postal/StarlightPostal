using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalloonMountInterractable : Interractable
{

    public AudioSource sfx_embark;

    private player p;
    private balloon b;

    void Start()
    {
        p = GameObject.Find("player").GetComponent<player>();
        b = GameObject.Find("balloon").GetComponent<balloon>();
    }
    
    public override void OnPlayerInterract()
    {
        if (!p.inBalloon && !b.lockEntry)
        {
            p.inBalloon = true;
            sfx_embark.Play(0);
        }
    }

    public override bool CanPlayerInterract()
    {
        return !b.lockEntry;
    }

}
