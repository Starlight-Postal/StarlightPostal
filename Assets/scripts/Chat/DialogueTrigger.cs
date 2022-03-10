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
                chatButton.visible = true;
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
                inMenu = false;

            }
        }

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            inMenu = false;
            chatButton.visible = false;
            rve.visible = inMenu;
            counter = 0;
            Script.text = script[counter];
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
            rve.visible = false;
            inMenu = false;
            chatButton.visible = false;
            sideButton.visible = false;
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
        
        if (counter == 10 || ((counter < 26) && (counter > 11)))
        {
            chatButton.visible = false;
        }
        
        //for tutorial NPC makes it so the script can move on it player has done the command
        if (player.inBalloon)
        {
            if (
            (counter == 10) ||
            (Input.GetKeyDown("w") && counter == 12) //||
           // (Input.GetKeyDown("s") && counter == 14) ||
         //   ((Input.GetKeyDown("a") || (Input.GetKeyDown("d"))) && counter == 17)
            )
            {
                chatButton.visible = true;
                counter++;
                Script.text = script[counter];
            }
        }
        if ((counter < 21 &&counter > 12) && (player.transform.position.x > checkpoint))
        {
            chatButton.visible = false;
            checkpoint = checkpoint + 45;
            counter++;
            Script.text = script[counter];

        }
        else if ((21 == counter) && (player.transform.position.x > 550))
        {
            counter++;
            Script.text = script[counter];
        }
    }
}



