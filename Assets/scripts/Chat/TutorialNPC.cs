using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialNPC : Interractable
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // private Story currentStory;
    public bool playerInRange;

    public player player;
    public Transform playerTrans;
    public balloon balloon;
    public Transform balloonTrans;
    public GameObject anchorObj;
    public anchor anchor;
    public bool anchored;
    //public PostOfficeClerk clerk;


    public bool inMenu = false;

    // Start is called before the first frame update
    private Button chatButton;
    private Label Script;
    public int counter;
    public int checkpoint = 16;

    public bool canNext = true;

    [SerializeField] private VisualElement rve;

    //bool's for players they talked too
    private bool firstNPC = false;

    public string[] script;
    public bool canLeave;

    public Transform trans;
    public GameObject body;
    public Transform bodyTrans;

    public int phase = 0;
    public float walkSpeed = 0.05f;

    public SpriteRenderer sprite;
    public bool facingRight = false;
    public string aniMode = "idle";
    public float aniSpeed = 0.25f;
    public Sprite[] aniIdle;
    public float aniIdleSpeed = 0.25f;
    public Sprite[] aniWalk;
    public float aniWalkSpeed = 1f;
    public float aniFrame = 0;

    private bool pressedDown = false;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();
        playerTrans = player.GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        balloonTrans = GameObject.Find("Center").GetComponent<Transform>();
        anchor = balloon.anchor;
        phase = 0;
        trans.position = new Vector3(2.2f,2.68f,0);
        bodyTrans = body.GetComponent<Transform>();

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

    public override void OnPlayerInterract()
    {
        turnOnDisplay();
        //sideButton.visible = true;
        Script.text = script[counter];
    }

    private void FixedUpdate()
    {
        //activates the text bubble if player in range
        if (rve.visible)
        {
            visualCue.SetActive(false);
            if (canNext)
            {
                if (player.kiSPACE == 1)
                {
                    counter++;
                    Script.text = script[counter];
                    if (counter == script.Length - 1) { chatButton.text = "space"; }
                    else { chatButton.text = "space"; }
                }
            }
        } else {
            if (playerInRange)
            {
                if (phase == 0 || phase == 8 || phase == 15||phase==16||phase==20||phase==21)
                {
                    visualCue.SetActive(true);
                    if (player.kiSPACE == 1)
                    {
                        //counter = 0;
                        turnOnDisplay();

                        Script.text = script[counter];
                    }
                } else
                {
                    visualCue.SetActive(false);
                }
            }
            //Checks to see if players are in range. If they arn't and chat should disappear it does
            else
            {
                visualCue.SetActive(false);
                if (playerInRange == false && canLeave == false)
                {
                    counter = 0;
                    Script.text = script[counter];
                    turnOffDisplay();
                }
            }
        }

        

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            turnOffDisplay();
            balloon.lockEntry = false;
        }

        
        rve.visible = inMenu;

        

        if (playerTrans.position.x > trans.position.x)
        {
            facingRight = true;
        }
        if (playerTrans.position.x < trans.position.x)
        {
            facingRight = false;
        }

        if (canLeave) { playerCanLeave(); }

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

        if (facingRight)
        {
            bodyTrans.localScale += new Vector3((-0.35f - bodyTrans.localScale.x), 0, 0) * 0.1f;
        } else
        {
            bodyTrans.localScale += new Vector3((0.35f - bodyTrans.localScale.x), 0, 0) * 0.1f;
        }
        
        pressedDown = false;
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        chatButton = rve.Q<Button>("chatButton");
        Script = rve.Q<Label>("chatLabel");

        chatButton.RegisterCallback<ClickEvent>(ev =>
        {
            if (chatButton.visible == true)
            {
                counter++;
                Script.text = script[counter];
                if (counter == script.Length - 1) { chatButton.text = "space"; }
                else { chatButton.text = "space"; }
            }
        }
        );
        rve.visible = false;

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void playerCanLeave()
    {
        aniMode = "idle";
        if (counter == 1)
        {
            balloon.lockEntry = true;
        }
        if (counter == 6)
        {
            if(phase == 0)
            {
                phase = 1;
                turnOffDisplay();
            }
            
        }
        if (phase == 1)
        {
            facingRight = false;
            if (walkTo(1.75f, 2.68f, walkSpeed))
            {
                phase = 2;
            }
        }
        if (phase == 2)
        {
            facingRight = false;
            if (walkTo(-0.54f, 0.58f, walkSpeed))
            {
                phase = 3;
            }
        }
        if (phase == 3)
        {
            facingRight = false;
            if (walkTo(-4f, 0.58f, walkSpeed))
            {
                phase = 4;
            }
        }
        if (phase == 4)
        {
            facingRight = false;
            if (walkTo(-4.55f, 0.44f, walkSpeed))
            {
                phase = 5;
            }
        }
        if (phase == 5)
        {
            facingRight = false;
            if (walkTo(-7.7f, 0.44f, walkSpeed))
            {
                phase = 6;
            }
        }
        if (phase == 6)
        {
            facingRight = false;
            if (walkTo(-9f, 0.75f, walkSpeed))
            {
                phase = 7;
            }
        }
        if (phase == 7)
        {
            facingRight = false;
            if (walkTo(-9.5f, 0.75f, walkSpeed))
            {
                phase = 8;
            }
        }

        if (phase == 8)
        {
            if (counter < 6)
            {
                counter = 6;
                Script.text = script[counter];
            }

            if (counter == 9)
            {
                balloon.lockEntry = false;
                canNext = false;
            }
            if (counter >9)
            {
                phase = 9;
                player.GetComponent<player>().inBalloon = true;
            }
        }
        //80 170 280
        if (phase == 9)
        {
            if (counter == 16)
            {
                Debug.Log("phase 10 " + balloonTrans.position.x);
                phase = 10;
                if (balloonTrans.position.x < 80)
                {
                    turnOffDisplay();
                }
            }
        }
        if (phase == 10)
        {
            if (balloonTrans.position.x >= 80)
            {
                turnOnDisplay();
                if (counter == 18)
                {
                    Debug.Log("phase 11 " + balloonTrans.position.x);
                    phase = 11;
                    if (balloonTrans.position.x < 170)
                    {
                        turnOffDisplay();
                    }
                }
            }
        }
        if (phase == 11)
        {
            if (balloonTrans.position.x >= 170)
            {
                turnOnDisplay();
                if (counter == 20)
                {
                    Debug.Log("phase 12 "+ balloonTrans.position.x);
                    phase = 12;
                    if (balloonTrans.position.x < 550)
                    {
                        turnOffDisplay();
                    }
                }
            }
        }
        if (phase == 12)
        {
            if (balloonTrans.position.x >= 550)
            {
                turnOnDisplay();
            }
        }

        if (counter == 9 || counter == 11 || counter == 14 || counter == 21 || counter == 22 || counter == 23 || counter == 24)
        {
            chatButton.visible = false;
            canNext = false;
        }

        //for tutorial NPC makes it so the script can move on it player has done the command
        if (player.inBalloon)
        {
            if(counter == 9)
            {
                chatButton.visible = true;
                canNext = true;
                counter++;
                Script.text = script[counter];
            }
            if (counter == 11 && balloon.th >= 10){
                chatButton.visible = true;
                canNext = true;
                counter++;
                Script.text = script[counter];
            }
            if (counter == 14 && pressedDown){
                chatButton.visible = true;
                canNext = true;
                counter++;
                Script.text = script[counter];
            }
        }
        /*if ((counter < 19 && counter > 11) && (player.transform.position.x > checkpoint)&&canNext)
        {
            //chatButton.visible = false;
            //canNext = false;
            checkpoint = checkpoint + 50;
            counter++;
            Script.text = script[counter];

        }*/
        if (counter<20&&(player.transform.position.x > 550))
        {
            counter = 20;
            Script.text = script[counter];
        }

        if (25 > counter && counter > 20)
        {
            if (player.inBalloon)
            {
                if (counter == 21 && balloon.th<=55) { counter++; Script.text = script[counter]; }
                if (anchor.stuck&&!anchor.landed) { counter = 23; Script.text = script[counter]; }
                else if (anchor.landed) { counter = 24; Script.text = script[counter]; }
            }
            else if (!player.inBalloon)
            {
                counter = 25;
                Script.text = script[counter];
                chatButton.visible = true;
                canNext = true;
                chatButton.text = "space";
                if (phase < 13)
                {
                    phase = 13;
                    trans.position = new Vector3(577.5f, 39.85f, 0);
                }
            }
        }
        if (phase > 8 && phase < 13)
        {
            body.SetActive(false);
        } else
        {
            body.SetActive(true);
        }
        if (counter == 25)
        {
            balloon.lockEntry = true;
        }
        if (counter == 26)
        {
            balloon.lockEntry = false;
            if (phase == 13)
            {
                phase = 14;
                turnOffDisplay();
            }
        }
        if (phase == 14)
        {
            turnOffDisplay();
            facingRight = true;
            if (walkTo(583.5f, 39.85f, walkSpeed))
            {
                phase = 15;
            }
        }
        if (phase == 15)
        {
            if (counter > 26)
            {
                counter = 26;
                Script.text = script[counter];
                turnOffDisplay();
            }
            /*if (clerk.playerDone)
            {
                phase = 16;
                counter = 27;
                Script.text = script[counter];
            }*/
        }
        if (phase == 16) {
            if (counter == 29)
            {
                phase = 17;
                    turnOffDisplay();
            }
        }
        if (phase == 17)
        {
            turnOffDisplay();
            facingRight = true;
            if (walkTo(619, 39.85f, walkSpeed))
            {
                phase = 18;
            }
        }
        if (phase == 18)
        {
            facingRight = true;
            if (walkTo(619.7f, 40.1f, walkSpeed))
            {
                phase = 19;
            }
        }
        if (phase == 19)
        {
            facingRight = true;
            if (walkTo(622.6f, 40.1f, walkSpeed))
            {
                phase = 20;
            }
        }
        if(phase == 20)
        {
            if (counter == 31)
            {
                counter = 29;
                Script.text = script[counter];
                turnOffDisplay();
            }
            /*if (clerk.delivered)
            {
                phase = 21;
                counter = 31;
            }*/
        }
        if(phase == 21)
        {
            if (counter > 31)
            {
                counter = 31;
                Script.text = script[counter];
                turnOffDisplay();
            }
        }
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
            trans.position += d;
            return true;
        }
    }

    public void turnOffDisplay()
    {
        rve.visible = false;
        inMenu = false;
        chatButton.visible = false;
        canNext = false;
    }
    public void turnOnDisplay()
    {
        rve.visible = true;
        inMenu = true;
        chatButton.visible = true;
        canNext = true;
    }

    void OnBurnVent(float axis) {
        if (axis < 0) {
            pressedDown = true;
        }
    }
}



