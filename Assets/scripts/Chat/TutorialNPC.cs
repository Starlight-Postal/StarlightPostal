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
    [SerializeField] private VisualElement rve;

    //bool's for players they talked too
    private bool firstNPC = false;

    public string[] script;
    public bool canLeave;

    //public Transform trans;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        anchor = balloon.anchor;
    }



    private void Update()
    {
        //activates the text bubble if player in range
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown("c"))
            {
                counter = 0;
                turnOnDisplay();
                sideButton.visible = true;

                Script.text = script[counter];
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

        if (canLeave) { playerCanLeave(); }
        rve.visible = inMenu;
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
                if (counter == script.Length - 1) { chatButton.text = "End"; }
                else { chatButton.text = "Next"; }
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

        if (counter == 9 || ((counter < 25) && (counter > 10)))
        {
            chatButton.visible = false;
        }

        //for tutorial NPC makes it so the script can move on it player has done the command
        if (player.inBalloon)
        {
            if (
            (counter == 9) ||
            (Input.GetKeyDown("w") && counter == 11))
            {
                chatButton.visible = true;
                counter++;
                Script.text = script[counter];
            }
        }
        if ((counter < 19 && counter > 11) && (player.transform.position.x > checkpoint))
        {
            chatButton.visible = false;
            checkpoint = checkpoint + 50;
            counter++;
            Script.text = script[counter];

        }
        if ((player.transform.position.x > 550))
        {
            counter = 21;
            Script.text = script[counter];
        }

        if (25 > counter && counter > 20)
        {
            if (player.inBalloon)
            {
                if (counter == 21 && Input.GetKeyDown("s")) { counter++; Script.text = script[counter]; }
                if (!anchor.landed) { counter = 23; Script.text = script[counter]; }
                else if (anchor.landed) { counter = 24; Script.text = script[counter]; }
            }
            else if (!player.inBalloon)
            {
                counter = 25;
                Script.text = script[counter];
                chatButton.visible = true;
                chatButton.text = "End";
            }
        }
    }

    public void turnOffDisplay()
    {
        rve.visible = false;
        inMenu = false;
        chatButton.visible = false;
        sideButton.visible = false;
    }
    public void turnOnDisplay()
    {
        rve.visible = true;
        inMenu = true;
        chatButton.visible = true;
    }
}



