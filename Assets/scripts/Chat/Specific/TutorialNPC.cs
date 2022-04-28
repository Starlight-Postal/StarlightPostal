using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialPhase
{
    INTRO,
    BALLOON,
    POSTOFFICE,
    POSTOFFICEDELIVERY,
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
    private bool facingLeft = false;
    private string aniMode = "idle";
    private float aniSpeed = 0.25f;
    private Sprite[] aniIdle;
    private float aniIdleSpeed = 0.1f;
    private Sprite[] aniWalk;
    private float aniWalkSpeed = 0.2f;
    private float aniFrame = 0;
    public float walkSpeed = 0.025f;

    private float prevTH;

    public TutorialPhase phase = TutorialPhase.INTRO;
    private int subphase;

    private FacePlayer face;

    public string[] scriptIntro;
    public string[] scriptBalloon;
    public string[] scriptPostOffice;
    public string[] scriptPostOfficeDelivery;
    public string[] scriptBar;
    public string[] scriptDelivered;

    public EncounterNPC recipient;

    private void Start()
    {
        base.Start();
        AnimationStart();

        balloonTrans = GameObject.Find("Center").transform;
        balloonScript = GameObject.Find("balloon").GetComponent<balloon>();
        playerScript = GameObject.Find("player").GetComponent<player>();
        face = GetComponent<FacePlayer>();
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
        if (phase == TutorialPhase.BALLOON && !playerScript.inBalloon && balloonTrans.position.x >= 550)
        {
            if (scriptIndex < 19)
            {
                scriptIndex = 19;
                OnConversationLineUpdate(19);
                chatScript.text = script[scriptIndex];
            }
        }
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
        face.facePlayer = false;
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
            face.facePlayer = true;
            return true;
        }
    }

    private bool WalkToLandingPad()
    {
        face.facingLeft = true;
        switch (subphase)
        {
            case 0:
                if (walkTo(1.9f, 2.25f, walkSpeed))
                    subphase++;
                return false;
            case 1:
                if (walkTo(-0.17f, 0.158f, walkSpeed))
                    subphase++;
                return false;
            case 2:
                if (walkTo(-3.84f, 0.158f, walkSpeed))
                    subphase++;
                return false;
            case 3:
                if (walkTo(-4.17f, 0.0f, walkSpeed))
                    subphase++;
                return false;
            case 4:
                if (walkTo(-7.5f, 0.0f, walkSpeed))
                    subphase++;
                return false;
            case 5:
                if (walkTo(-8.74f, 0.32f, walkSpeed))
                    subphase++;
                return false;
            case 6:
                if (walkTo(-9.5f, 0.32f, walkSpeed))
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
        face.facingLeft = false;
        return walkTo(583.5f, 39.42f, walkSpeed);
    }

    private bool DadGetsMilkFromDownTheStreet()
    {
        face.facingLeft = false;
        switch (subphase) {
            case 0:
                if (walkTo(619.3f, 39.42f, walkSpeed))
                    subphase++;
                return false;
            case 1:
                if (walkTo(619.7f, 39.67f, walkSpeed))
                    subphase++;
                return false;
            case 2:
                if (walkTo(622.6f, 39.67f, walkSpeed))
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
        if (isTalking)
        {
            AdvanceScript();
        }
    }

    public override bool ResetOnPlayerLeave(int index)
    {
        return false;
    }

    public override void OnConversationStart()
    {
        if (recipient.deliveredTo)
        {
            if (phase == TutorialPhase.POSTOFFICE)
            {
                phase = TutorialPhase.POSTOFFICEDELIVERY;
            } else if(phase == TutorialPhase.BAR)
            {
                phase = TutorialPhase.DELIVERED;
            }
        }
        switch (phase)
        {
            default:
            case TutorialPhase.INTRO:
                balloonScript.lockEntry = true;
                script = scriptIntro;
                break;
            case TutorialPhase.BALLOON:
                balloonScript.lockEntry = false;
                script = scriptBalloon;
                break;
            case TutorialPhase.POSTOFFICE:
                script = scriptPostOffice;
                break;
            case TutorialPhase.POSTOFFICEDELIVERY:
                script = scriptPostOfficeDelivery;
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
            case TutorialPhase.POSTOFFICEDELIVERY:
                phase = TutorialPhase.BAR;
                break;
        }
        scriptIndex = 0;
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (phase == TutorialPhase.BALLOON && index == 4)
        {
            body.SetActive(false);
            balloonScript.captainIsWith = true;
        }
        if (phase == TutorialPhase.BALLOON && index == 8)
        {
            prevTH = balloonScript.th;
        }
        if (phase == TutorialPhase.BALLOON && index == 19)
        {
            trans.position = new Vector3(578.0f, 39.42f, 0);
            body.SetActive(true);
            balloonScript.captainIsWith = false;
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
                        if (balloonScript.th > prevTH)
                        {
                            prevTH = balloonScript.th;
                        }
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
            case TutorialPhase.POSTOFFICEDELIVERY:
                return index == 2;
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
                    case 11:
                        return balloonTrans.position.x >= 80;
                    case 12:
                        return balloonTrans.position.x >= 170;
                    case 14:
                        return balloonTrans.position.x >= 550;
                    case 20:
                        return WalkToPostOffice();
                }
                break;
            case TutorialPhase.POSTOFFICEDELIVERY:
            case TutorialPhase.DELIVERED:
                if (index == 2)
                    return DadGetsMilkFromDownTheStreet();
                break;
        }
        
        return true;
    }

    // Prevents the player script from finding grandpa when the "press space to get in" message is showing,
    // allowing the interract event to go to the balloon even when the tutorial npc is closer
    public override bool CanPlayerInterract()
    {
        if (phase == TutorialPhase.BALLOON && scriptIndex >= 3)
        {
            return false;
        }
        if (!inMenu && isTalking)
        {
            return false;
        }
        return true;
    }

    public override bool AllowConcurrentConversations()
    {
        return !inMenu;
    }

    public override bool ZoomCamera()
    {
        return inMenu;
    }

}
