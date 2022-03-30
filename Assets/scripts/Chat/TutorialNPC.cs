using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialNPC : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // private Story currentStory;
    public bool playerInRange;

    public player player;
    public balloon balloon;
    public Transform balloonTrans;
    public GameObject anchorObj;
    public anchor anchor;
    public bool anchored;


    public bool inMenu = false;

    // Start is called before the first frame update
    private Button chatButton;
    private Button sideButton;
    private Label Script;
    private int counter;
    public int checkpoint = 16;

    public bool canNext = true;

    [SerializeField] private VisualElement rve;

    //bool's for players they talked too
    private bool firstNPC = false;

    public string[] script;
    public bool canLeave;

    public Transform trans;

    public int phase = 0;
    public float walkSpeed = 0.05f;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        balloonTrans = GameObject.Find("Center").GetComponent<Transform>();
        anchor = balloon.anchor;
        phase = 0;
        trans.position = new Vector3(2.2f,2.68f,0);
    }



    private void FixedUpdate()
    {
        //activates the text bubble if player in range
        if (playerInRange)
        {
            if (phase == 0||phase==8)
            {
                visualCue.SetActive(true);
                if (player.kiSPACE==1)
                {
                    //counter = 0;
                    turnOnDisplay();
                    sideButton.visible = true;

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

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            turnOffDisplay();
        }

        
        rve.visible = inMenu;

        if (rve.visible)
        {
            if (canNext)
            {
                if (player.kiSPACE == 1)
                {
                    counter++;
                    Script.text = script[counter];
                    if (counter == script.Length - 1) { chatButton.text = "space"; }
                    else { chatButton.text = "space"; }
                    sideButton.visible = false;
                }
            }
        }
        if (canLeave) { playerCanLeave(); }
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        chatButton = rve.Q<Button>("chatButton");
        Script = rve.Q<Label>("chatLabel");
        sideButton = rve.Q<Button>("sideButton");

        chatButton.RegisterCallback<ClickEvent>(ev =>
        {
            if (chatButton.visible == true)
            {
                counter++;
                Script.text = script[counter];
                if (counter == script.Length - 1) { chatButton.text = "space"; }
                else { chatButton.text = "space"; }
                sideButton.visible = false;
            }
        }
        );
        sideButton.RegisterCallback<ClickEvent>(ev =>
        {
            counter = 26;
            turnOffDisplay();
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
            if (walkTo(1.75f, 2.68f, walkSpeed))
            {
                phase = 2;
            }
        }
        if (phase == 2)
        {
            if (walkTo(-0.54f, 0.58f, walkSpeed))
            {
                phase = 3;
            }
        }
        if (phase == 3)
        {
            if (walkTo(-4f, 0.58f, walkSpeed))
            {
                phase = 4;
            }
        }
        if (phase == 4)
        {
            if (walkTo(-4.55f, 0.44f, walkSpeed))
            {
                phase = 5;
            }
        }
        if (phase == 5)
        {
            if (walkTo(-7.7f, 0.44f, walkSpeed))
            {
                phase = 6;
            }
        }
        if (phase == 6)
        {
            if (walkTo(-9f, 0.75f, walkSpeed))
            {
                phase = 7;
            }
        }
        if (phase == 7)
        {
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
            }
            if (counter >9)
            {
                phase = 9;
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
            if (counter == 14 && (Input.GetKey("down")||Input.GetKey("s"))){
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
            }
        }
    }

    public bool walkTo(float x,float y,float s)
    {
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
        sideButton.visible = false;
        canNext = false;
    }
    public void turnOnDisplay()
    {
        rve.visible = true;
        inMenu = true;
        chatButton.visible = true;
        canNext = true;
    }
}



