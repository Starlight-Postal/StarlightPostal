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

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();

    }

    /*
        if (Input.GetKeyDown("i") && playerInRange)
        {
            inMenu = true;
            counter = 0;
            rve.visible = inMenu;

        }
        */
    /*     {
             if (playerInRange == false) 
             {
                 counter = 0;
                 nextLine();
             }
         }

             if (playerInRange)
            {
                // dialoguePanel.SetActive(true);

                visualCue.SetActive(true);
            }
     */

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown("i"))
            {
                counter = 0;
                inMenu = true;
                rve.visible = inMenu;
            }
        }
        else
        {
            visualCue.SetActive(false);
            if (playerInRange == false)
            {
                counter = 0;
                nextLine();
            }
        }
    
        rve.visible = inMenu;
    }
    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        ChatButton = rve.Q<Button>("ChatButton");
        Script = rve.Q<Label>("chatLabel");

        ChatButton.RegisterCallback<ClickEvent>(ev => { counter++; nextLine(); });
        rve.visible = false;

    }


    private void nextLine()
    {
        if (counter == 1) { Script.text = "I hate to say this, but we are running behind on mail"; }
        else if (counter == 2) { Script.text = "we would like for you to start right away"; }
        else if (counter == 3) 
        { 
            Script.text = "Hurry, up and fly up to the first town with the mail";
            firstNPC = true;
        }
        else
        {
            Script.text = "Hello,____ glad to see you getting here nice and safly.";
            inMenu = false;
            rve.visible = inMenu;
            counter = 0;
        }

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


