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


    public global_data gdata;
    public global_data balloonNumber;

    private bool ownBalloon2;
    private bool ownBalloon3;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        balloonNumber = GameObject.Find("Coin Global Data").GetComponent<global_data>();
    }

    private void Update()
    {
        //activates the text bubble if player in range
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown("space"))
            {
                balloon.ChangeBalloonSkin("logo-1");
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
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
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
              if (!(balloonNumber.balloon == 1))
              {
                  balloonNumber.balloon = 1;
                  balloon.ChangeBalloonSkin("stripes-1");
              }
          });
          button2.RegisterCallback<ClickEvent>(ev =>
          {
              if (ownBalloon2 && !(balloonNumber.balloon == 2))
              {
                  balloonNumber.balloon = 2;
                  balloon.ChangeBalloonSkin("logo-1");
              }
          });
          button3.RegisterCallback<ClickEvent>(ev =>
          {
              if (ownBalloon3 && !(balloonNumber.balloon == 3))
              {
                  balloonNumber.balloon = 3;
                  balloon.ChangeBalloonSkin("epic");
              }
          });
  

        rve.visible = false;
    }

    private void updateShop()
    {
        if (balloonNumber.balloon == 1)
        {
            button1.text = "Equipped";
            button2.text = "Select";
            button3.text = "Select";
        }
        if (balloonNumber.balloon == 2)
        {
            button1.text = "Select";
            button2.text = "Equipped";
            button3.text = "Select";
        }
        if (balloonNumber.balloon == 3)
        {
            button1.text = "Select";
            button2.text = "Select";
            button3.text = "Equipped";
        }

    }
    private void unlockShopSkins()
    {
        if (gdata.coins > 50 && !(balloonNumber.balloon == 2))
        {
            ownBalloon2 = true;
            button2.text = "Select";
        }
        else { button2.text = "Buy for 50 stars"; }
        if (gdata.coins > 200 && !(balloonNumber.balloon == 3))
        {
            ownBalloon3 = true;
            button3.text = "Select";
        }
       else { button3.text = "Buy for 200 stars"; }
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
       // updateShop();
       // unlockShopSkins();
    }





}
