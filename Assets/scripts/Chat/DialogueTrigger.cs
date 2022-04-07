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

    public Transform trans;
    public player player;
    public Transform playerTrans;
    public balloon balloon;
    //public anchor anchor;

    public bool facingRight;
    public bool facePlayer = true;
    public Transform bodyTrans;
    public float visWidth;

    public bool canLeave = true;
    public bool encountered = false;


    public bool inMenu = false;

    // Start is called before the first frame update
    private Button chatButton;
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
        //anchor = balloon.anchor;
        playerTrans = player.GetComponent<Transform>();
        visWidth = bodyTrans.localScale.x;
        encountered = false;
    }



    private void FixedUpdate()
    {
        if (rve.visible)
        {
            if (player.kiSPACE == 1)
            {
                counter++;
                Script.text = script[counter];
                if (counter == script.Length - 1) { chatButton.text = "space"; }
                else { chatButton.text = "space"; }
            }
            if (!playerInRange)
            {
                if (canLeave)
                {
                    visualCue.SetActive(false);
                    counter = 0;
                    Script.text = script[counter];
                    inMenu = false;
                }
            }
        }
        else
        {
            //activates the text bubble if player in range
            if (playerInRange)
            {
                visualCue.SetActive(true);
                if (player.kiSPACE == 1)
                {
                    counter = 0;
                    inMenu = true;
                    rve.visible = inMenu;
                    Script.text = script[counter];
                    encountered = true;
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
        }

        //Gets rid of the chat if they hid next one more time
        if (counter >= script.Length)
        {
            inMenu = false;
            rve.visible = inMenu;
            counter = 0;
            Script.text = script[counter];
        }
        //if (counter == script.Length - 1) { chatButton.text = "End"; }

            rve.visible = inMenu;


        if (facePlayer)
        {
            if (playerTrans.position.x > trans.position.x)
            {
                facingRight = true;
            }
            if (playerTrans.position.x < trans.position.x)
            {
                facingRight = false;
            }
        }

        if (facingRight)
        {
            bodyTrans.localScale += new Vector3((visWidth - bodyTrans.localScale.x), 0, 0) * 0.1f;
        }
        else
        {
            bodyTrans.localScale += new Vector3((-visWidth - bodyTrans.localScale.x), 0, 0) * 0.1f;
        }

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




