using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // private Story currentStory;
    public bool playerInRange;

    public player player;
    public bool inMenu = false;

    // Start is called before the first frame update
    private Button ChatButton;
    private Label Script;
    private int counter;
    [SerializeField] private VisualElement rve;

    //bool's for players they talked too
    private bool firstNPC = false;

    public string[] script;
    public bool canLeave;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();

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
                inMenu = true;
                rve.visible = inMenu;
                Script.text = script[counter];

                //gets rid of the button for the tutorial NPC
                if (canLeave == true) { ChatButton.visible = false; }
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
                inMenu = false;
            }
        }

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            inMenu = false;
            rve.visible = inMenu;
            counter = 0;
            Script.text = script[counter];
        }


        //for tutorial NPC makes it so the script can move on it player has done the command
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("d")) && canLeave && counter == 0)
        {
            counter++;
            Script.text = script[counter];
        }
        else if (Input.GetKeyDown("s") && canLeave && counter == 1)
        {
            counter++;
            Script.text = script[counter];
        }
        else if (player.inBalloon && canLeave && counter == 2)
        {
            counter++;
            Script.text = script[counter];
        }
        else if (Input.GetKeyDown("w") && player.inBalloon && canLeave && counter == 3)
        {
            counter++;
            Script.text = script[counter];
        }
        else if (Input.GetKeyDown("s") && player.inBalloon && canLeave && counter == 4)
        {
            counter++;
            firstNPC == true;
        }
        rve.visible = inMenu;
    
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        ChatButton = rve.Q<Button>("ChatButton");
        Script = rve.Q<Label>("chatLabel");

        ChatButton.RegisterCallback<ClickEvent>(ev => 
        {
            counter++;
            Script.text = script[counter];
            if (counter == script.Length - 1) { ChatButton.text = "End"; }
            else { ChatButton.text = "Next"; }
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
}


