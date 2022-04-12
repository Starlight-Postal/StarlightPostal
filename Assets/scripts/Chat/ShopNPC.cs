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
    [SerializeField] public int stage;


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

    private bool ownBalloon2;
    private bool ownBalloon3;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("player").GetComponent<player>();
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        updateShop();
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
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        rve = GetComponent<UIDocument>().rootVisualElement;

        Shop = rve.Q<Label>("Shop");
        Shop1 = rve.Q<Label>("Shop1");
        Shop2 = rve.Q<Label>("Shop2");
        Shop3 = rve.Q<Label>("Shop3");

        button1 = rve.Q<Button>("Balloon1");
        button2 = rve.Q<Button>("Balloon2");
        button3 = rve.Q<Button>("Balloon3");

        button1.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.coins > 50 && !(gdata.balloon1 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 50;
                gdata.balloon1 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon4 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 50;
                gdata.balloon4 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon7 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 50;
                gdata.balloon7 = 1;
            }
        });
        button2.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.coins > 50 && !(gdata.balloon2 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 100;
                gdata.balloon2 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon5 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 100;
                gdata.balloon5 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon8 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 100;
                gdata.balloon8 = 1;
            }
        });
        button3.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.coins > 50 && !(gdata.balloon3 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon3 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon6 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon6 = 1;
            }
            else if (gdata.coins > 50 && !(gdata.balloon9 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon9 = 1;
            }
        });


        rve.visible = false;
    }

    private void updateShop()
    {
        if(stage == 1)
        { 
            if (gdata.balloon1 == 1) { button1.text = "Owned"; }
            else { button1.text = "Costs 50 stars"; }
            if (gdata.balloon1 == 4) { button2.text = "Owned"; }
            else { button2.text = "Costs 100 stars"; }
            if (gdata.balloon1 == 7) { button3.text = "Owned"; }
            else { button3.text = "Costs 150 stars"; }
        }
        if (stage == 2)
        {
            if (gdata.balloon2 == 2) { button1.text = "Owned"; }
            else { button1.text = "Costs 50 stars"; }
            if (gdata.balloon5 == 5) { button2.text = "Owned"; }
            else { button2.text = "Costs 100 stars"; }
            if (gdata.balloon8 == 8) { button3.text = "Owned"; }
            else { button3.text = "Costs 150 stars"; }
        }
        if (stage == 3)
        {
            if (gdata.balloon3 == 3) { button1.text = "Owned"; }
            else { button1.text = "Costs 50 stars"; }
            if (gdata.balloon6 == 6) { button2.text = "Owned"; }
            else { button2.text = "Costs 100 stars"; }
            if (gdata.balloon9 == 9) { button3.text = "Owned"; }
            else { button3.text = "Costs 150 stars"; }
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
        updateShop();
        //unlockShopSkins();
    }





}


// balloon.ChangeBalloonSkin("stripes-1");