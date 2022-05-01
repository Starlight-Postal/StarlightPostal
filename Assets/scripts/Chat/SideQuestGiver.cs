using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SideQuestGiver : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    // private Story currentStory;
    public bool playerInRange;

    public player player;
    public balloon balloon;
    public anchor anchor;
    public bool inMenu = false;
    public bool grabbedDog = false;

    // Start is called before the first frame update
    private Button yesButton;
    private Button noButton;
    private Label Script;
    private int counter;

    public int checkpoint;
    public int endtalking;
    public int whereToStart;

    [SerializeField] private VisualElement rve;

    //bool's for players they talked too
    public string[] script;

    //public Transform trans;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        inMenu = false;
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
            if (inMenu = true)
            {
                if (Input.GetKeyDown("space"))
                {
                    counter++;
                    Script.text = script[counter];
                }
            }
            else if (Input.GetKeyDown("space"))
            {
                inMenu = true;
                Script.text = script[counter];
            }
        }

        //Checks to see if players are in range. If they arn't and chat should disappear it does
        else
        {
            visualCue.SetActive(false);
            inMenu = false;
        }



        if (inMenu = true) { startUI(); }
        else { endUI(); }
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        yesButton = rve.Q<Button>("yesButton");
        Script = rve.Q<Label>("chatLabel");
        noButton = rve.Q<Button>("noButton");
        noButton.visible = false;

        yesButton.RegisterCallback<ClickEvent>(ev =>
        {
            if (yesButton.visible == true)
            {
                counter++;
                Script.text = script[counter];

            }
        }
        );
        noButton.RegisterCallback<ClickEvent>(ev =>
        {
            if (noButton.visible == true)
            {

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

    private void startUI()
    {
        rve.visible = true;
        if (!(counter == checkpoint))
        {
            noButton.visible = false;
            yesButton.text = "Space";

        }
        else
        {
            noButton.visible = true;
            yesButton.text = "Sure";
            noButton.text = "hell no";
        }
    }

    private void endUI()
    {
        // rve.visible = false;
        // noButton.visible = false;
        //counter = 0;
        // Script.text = script[counter];
    }
}




