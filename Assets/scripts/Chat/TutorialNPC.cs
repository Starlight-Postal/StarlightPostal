using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialPhase
{
    STARTING_POST_OFFICE,
    WALKING_TO_BALLOON,
    WAITING_AT_BALLOON,


}

public class TutorialNPC : Conversation
{

    private Transform balloonTrans;
    private balloon balloonScript;
    private player playerScript;

    public Transform trans;
    public GameObject body;
    private SpriteRenderer sprite;
    private bool facingRight = false;
    private string aniMode = "idle";
    private float aniSpeed = 0.25f;
    private Sprite[] aniIdle;
    private float aniIdleSpeed = 0.25f;
    private Sprite[] aniWalk;
    private float aniWalkSpeed = 1f;
    private float aniFrame = 0;
    public float walkSpeed = 0.05f;

    private TutorialPhase phase = TutorialPhase.STARTING_POST_OFFICE;
    private int subphase;

    private void Start()
    {
        base.Start();
        AnimationStart();

        balloonTrans = GameObject.Find("Center").transform;
        balloonScript = GameObject.Find("balloon").GetComponent<balloon>();
        playerScript = GameObject.Find("player").GetComponent<player>();
    }

    private void AnimationStart()
    {
        aniMode = "idle";
        aniFrame = 0;
        aniIdle = new Sprite[7];

        for (int i = 0; i < 7; i++)
        {
            aniIdle[i] = Resources.Load<Sprite>("textures/grandpa/grandpa_idle/grandpa_idle_" + i);
        }
        aniWalk = new Sprite[8];

        for (int i = 0; i < 8; i++)
        {
            aniWalk[i] = Resources.Load<Sprite>("textures/grandpa/grandpa_walk/grandpa_walk_" + i);
        }

        sprite = body.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        Sprite[] ani = aniIdle;
        switch (aniMode)
        {
            case "idle":
                ani = aniIdle;
                aniSpeed = aniIdleSpeed;
                break;
            case "walk":
                ani = aniWalk;
                aniSpeed = aniWalkSpeed;
                break;
            default:
                break;
        }

        aniFrame = aniFrame % ani.Length;
        sprite.sprite = ani[(int)aniFrame];
        aniFrame = (aniFrame + aniSpeed) % ani.Length;
    }

    public bool walkTo(float x,float y,float s)
    {
        aniMode = "walk";
        Vector3 d = new Vector3(x - trans.position.x, y - trans.position.y, 0);
        if (d.magnitude > s)
        {
            Vector3 m = (d / d.magnitude) * s;
            trans.position += m;
            return false;
        } else
        {
            aniMode = "idle";
            trans.position += d;
            return true;
        }
    }

    void OnLeaveBalloon()
    {
        AdvanceScript();
    }

    public override bool ResetOnComplete()
    {
        return false;
    }

    // For when we want the player to be able to advance to the next line
    public override bool CanPlayerContinue(int index)
    {
        switch (index)
        {
            case 9:
            case 11:
            //case 14:
            case 21:
            case 22:
            case 23:
            case 24:
                return false;
            default:
                return true;
        }
    }

    // For when we want to automatically advance to the next line
    public override bool AutoAdvanceConditionMet(int index)
    {
        switch (index)
        {
            case 9:
                return playerScript.inBalloon;
            case 11:
                return balloonTrans.position.y > 5.0f;
            case 14:
                //TODO
                return false;
            case 21:
                return balloonScript.th <= 55;
            case 22:
                return balloonScript.anchor.stuck;
            case 23:
                return balloonScript.anchor.landed;
            case 24:
                return !playerScript.inBalloon;
            default:
                return false;            
        }
    }

    // For when we want the ui to disappear and wait for conditions
    public override bool ReadyToAdvanceTo(int index)
    {
        Debug.Log(phase);
        Debug.Log(subphase);
        switch (index)
        {
            case 6:
                facingRight = false;
                switch (subphase)
                {
                    case 0:
                        if (walkTo(1.75f, 2.68f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 1:
                        if (walkTo(-0.54f, 0.58f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 2:
                        if (walkTo(-4f, 0.58f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 3:
                        if (walkTo(-4.55f, 0.44f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 4:
                        if (walkTo(-7.7f, 0.44f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 5:
                        if (walkTo(-9f, 0.75f, walkSpeed))
                            subphase++;
                        return false;
                        break;
                    case 6:
                        if (walkTo(-9.5f, 0.75f, walkSpeed))
                        {
                            subphase = 0;
                            phase = TutorialPhase.WAITING_AT_BALLOON;
                            return true;
                        }
                        return false;
                        break;
                }
                break;
            case 18:
                return balloonTrans.position.x >= 80;
            case 20:
                return balloonTrans.position.x >= 550;
            default:
                return true;
        }
        return true;
    }

}