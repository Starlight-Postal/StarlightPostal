using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopNPC : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;

    public player player;
    public balloon balloon;
    public anchor anchor;


    public bool inMenu = false;

    //    Start is called before the first frame update
    private Button button1;
    private Button button2;
    private Button button3;
    private Label Shop;
    private Label Shop1;
    private Label Shop2;
    private Label Shop3;

    [SerializeField] private VisualElement rve;

//    public string[] script;

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
            if (Input.GetKeyDown("space"))
            {
                inMenu = true;
            }
        }

        //Checks to see if players are in range. If they arn't and chat should disappear it does
        else
        {
            visualCue.SetActive(false);
            inMenu = false;
        }

        if (inMenu) { openShop(); }
        else { closeShop(); }
    }

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;

        Shop = rve.Q<Label>("Shop");
        Shop1 = rve.Q<Label>("Shop1");
        Shop2 = rve.Q<Label>("Shop2");
        Shop3 = rve.Q<Label>("Shop3");
        
        button1 = rve.Q<Button>("Balloon1");
        button2 = rve.Q<Button>("Balloon2");
        button3 = rve.Q<Button>("Balloon3");
        /*
        Sonic = rve.Q<Label>("Sonic");
        Logo = rve.Q<Label>("Logo");
        Starting = rve.Q<Label>("Starting");
        */

        button1.RegisterCallback<ClickEvent>(ev =>
          {

          });
          button2.RegisterCallback<ClickEvent>(ev =>
          {

          });
          button3.RegisterCallback<ClickEvent>(ev =>
          {

          });
  

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

    private void closeShop()
    {
        Shop = rve.Q<Label>("Shop");
        Shop1 = rve.Q<Label>("Shop1");
        Shop2 = rve.Q<Label>("Shop2");
        Shop3 = rve.Q<Label>("Shop3");

        button1 = rve.Q<Button>("Balloon1");
        button2 = rve.Q<Button>("Balloon2");
        button3 = rve.Q<Button>("Balloon3");
        rve.visible = false;
    }

    private void openShop()
    {
        Shop = rve.Q<Label>("Shop");
        Shop1 = rve.Q<Label>("Shop1");
        Shop2 = rve.Q<Label>("Shop2");
        Shop3 = rve.Q<Label>("Shop3");

        button1 = rve.Q<Button>("Balloon1");
        button2 = rve.Q<Button>("Balloon2");
        button3 = rve.Q<Button>("Balloon3");
        rve.visible = true;
    }





}
