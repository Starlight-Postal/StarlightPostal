using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialPhase
{
    INTRO,
    BALLOON,
    POSTOFFICE,
    BAR,
    DELIVERED
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

    private float prevTH;

    public TutorialPhase phase = TutorialPhase.INTRO;
    private int subphase;

    public string[] scriptIntro;
    public string[] scriptBalloon;
    public string[] scriptPostOffice;
    public string[] scriptBar;
    public string[] scriptDelivered;

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

    private bool WalkToLandingPad()
    {
        facingRight = false;
        switch (subphase)
        {
            case 0:
                if (walkTo(1.75f, 2.68f, walkSpeed))
                    subphase++;
                return false;
            case 1:
                if (walkTo(-0.54f, 0.58f, walkSpeed))
                    subphase++;
                return false;
            case 2:
                if (walkTo(-4f, 0.58f, walkSpeed))
                    subphase++;
                return false;
            case 3:
                if (walkTo(-4.55f, 0.44f, walkSpeed))
                    subphase++;
                return false;
            case 4:
                if (walkTo(-7.7f, 0.44f, walkSpeed))
                    subphase++;
                return false;
            case 5:
                if (walkTo(-9f, 0.75f, walkSpeed))
                    subphase++;
                return false;
            case 6:
                if (walkTo(-9.5f, 0.75f, walkSpeed))
                {
                    subphase = 0;
                    return true;
                }
                return false;
        }
        return true;
    }

    private bool WalkToPostOffice()
    {
        return walkTo(583.5f, 39.85f, walkSpeed);
    }

    private bool DadGetsMilkFromDownTheStreet()
    {
        facingRight = true;
        switch (subphase) {
            case 0:
                if (walkTo(619, 39.85f, walkSpeed))
                    subphase++;
                return false;
            case 1:
                if (walkTo(619.7f, 40.1f, walkSpeed))
                    subphase++;
                return false;
            case 2:
                if (walkTo(622.6f, 40.1f, walkSpeed))
                {
                    subphase = 0;
                    return true;
                }
                return false;
        }
        return true;
    }

    void OnLeaveBalloon()
    {
        AdvanceScript();
    }

    public override void OnConversationStart()
    {
        switch (phase)
        {
            default:
            case TutorialPhase.INTRO:
                script = scriptIntro;
                break;
            case TutorialPhase.BALLOON:
                script = scriptBalloon;
                break;
            case TutorialPhase.POSTOFFICE:
                script = scriptPostOffice;
                break;
            case TutorialPhase.BAR:
                script = scriptBar;
                break;
            case TutorialPhase.DELIVERED:
                script = scriptDelivered;
                break;
        }
    }

    public override void OnConversationEnd()
    {
        switch (phase)
        {
            case TutorialPhase.INTRO:
                phase = TutorialPhase.BALLOON;
                break;
            case TutorialPhase.BALLOON:
                phase = TutorialPhase.POSTOFFICE;
                break;
            case TutorialPhase.POSTOFFICE:
                phase = TutorialPhase.BAR;
                break;
        }
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (phase == TutorialPhase.BALLOON && index == 4)
        {
            body.SetActive(false);
        }
        if (phase == TutorialPhase.BALLOON && index == 8)
        {
            prevTH = balloonScript.th;
        }
        if (phase == TutorialPhase.BALLOON && index == 19)
        {
            trans.position = new Vector3(577.5f, 39.85f, 0);
            body.SetActive(true);
        }
    }

    // For when we want the player to be able to advance to the next line
    public override bool CanPlayerContinue(int index)
    {
        if (phase == TutorialPhase.BALLOON)
        {
            switch (index)
            {
                case 3:
                case 5:
                case 8:
                case 15:
                case 16:
                case 17:
                case 18:
                    return false;
            }
        }
        return true;
    }

    // For when we want to automatically advance to the next line
    public override bool AutoAdvanceConditionMet(int index)
    {
        switch (phase)
        {
            case TutorialPhase.INTRO:
                return index == 6;
            case TutorialPhase.BALLOON:
                switch (index)
                {
                    case 3:
                        return playerScript.inBalloon;
                    case 5:
                        return balloonTrans.position.y > 5.0f;
                    case 8:
                        return prevTH - 2.0f > balloonScript.th;
                    case 15:
                        return balloonScript.th <= 55;
                    case 16:
                        return balloonScript.anchor.stuck;
                    case 17:
                        return balloonScript.anchor.landed;
                    case 18:
                        return !playerScript.inBalloon;
                    case 20:
                        return true;    
                }
                break;
            case TutorialPhase.POSTOFFICE:
                return index == 3;
        }

        return false;        
    }

    // For when we want the ui to disappear and wait for conditions
    public override bool ReadyToAdvanceTo(int index)
    {
        switch (phase)
        {
            case TutorialPhase.INTRO:
                if (index == 6)
                    return WalkToLandingPad();
                break;
            case TutorialPhase.BALLOON:
                switch (index)
                {
                    case 12:
                        return balloonTrans.position.x >= 80;
                    case 14:
                        return balloonTrans.position.x >= 550;
                    case 20:
                        return WalkToPostOffice();
                }
                break;
            case TutorialPhase.POSTOFFICE:
                if (index == 3)
                    return DadGetsMilkFromDownTheStreet();
                break;
        }
        
        return true;
    }

}