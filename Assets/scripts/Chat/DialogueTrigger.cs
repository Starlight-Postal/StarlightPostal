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
    public balloon balloon;
    public anchor anchor;


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
                inMenu = true;
                rve.visible = inMenu;
                Script.text = script[counter];
            }
        }

        //Checks to see if players are in range. If they arn't and chat should disappear it does
        else
        {
            visualCue.SetActive(false);
            counter = 0;
            Script.text = script[counter];
            inMenu = false;
        }

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            inMenu = false;
            rve.visible = inMenu;
            counter = 0;
            Script.text = script[counter];
        }
        if (counter == script.Length - 1) { chatButton.text = "End"; }

            rve.visible = inMenu;
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        chatButton = rve.Q<Button>("chatButton");
        Script = rve.Q<Label>("chatLabel");
        sideButton = rve.Q<Button>("sideButton");
        sideButton.visible = false;

        chatButton.RegisterCallback<ClickEvent>(ev =>
        {
            if (chatButton.visible == true)
            {
                counter++;
                Script.text = script[counter];
                if (counter == script.Length - 1) { chatButton.text = "End"; }
                else { chatButton.text = "Next"; }
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

 
}




