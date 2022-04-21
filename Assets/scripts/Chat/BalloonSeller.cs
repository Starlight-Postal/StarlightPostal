using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class BalloonSeller : Interractable
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;
    [SerializeField] public int stage;


    public balloon balloon;


    public bool inMenu = false;

    //    Start is called before the first frame update
    private Button button1;
    private Button button2;
    private Button button3;
    private Label Shop;
    private Label Shop1;
    private Label Shop2;
    private Label Shop3;

    private Label skin1;
    private Label skin2;
    private Label skin3;
    private Label skin4;
    private Label skin5;
    private Label skin6;
    private Label skin7;
    private Label skin8;
    private Label skin9;


    [SerializeField] private VisualElement rve;


    public global_data gdata;

    private bool ownBalloon2;
    private bool ownBalloon3;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        updateShop();
    }

    public override void OnPlayerInterract()
    {
        openShop();
        inMenu = true;
        updateStage();
        turnoffimage();
        Displayimage();
    }

    private void Update()
    {
        //activates the text bubble if player in range
        if (playerInRange)
        {
            visualCue.SetActive(true);
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

        skin1 = rve.Q<Label>("skin1");
        skin2 = rve.Q<Label>("skin2");
        skin3 = rve.Q<Label>("skin3");
        skin4 = rve.Q<Label>("skin4");
        skin5 = rve.Q<Label>("skin5");
        skin6 = rve.Q<Label>("skin6");
        skin7 = rve.Q<Label>("skin7");
        skin8 = rve.Q<Label>("skin8");
        skin9 = rve.Q<Label>("skin9");
        button1.RegisterCallback<ClickEvent>(ev =>
        {

            if (gdata.coins >= 150 && !(gdata.balloon1 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon1 = 1;
            }
            else if (gdata.coins >= 150 && !(gdata.balloon4 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon4 = 1;
            }
            else if (gdata.coins >= 150 && !(gdata.balloon7 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 150;
                gdata.balloon7 = 1;
            }
        });
        button2.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.coins >= 200 && !(gdata.balloon2 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 200;
                gdata.balloon2 = 1;
            }
            else if (gdata.coins >= 200 && !(gdata.balloon5 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 200;
                gdata.balloon5 = 1;
            }
            else if (gdata.coins >= 200 && !(gdata.balloon8 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 200;
                gdata.balloon8 = 1;
            }
        });
        button3.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.coins >= 250 && !(gdata.balloon3 == 1) && stage == 1)
            {
                gdata.coins = gdata.coins - 250;
                gdata.balloon3 = 1;
            }
            else if (gdata.coins >= 350 && !(gdata.balloon6 == 1) && stage == 2)
            {
                gdata.coins = gdata.coins - 350;
                gdata.balloon6 = 1;
            }
            else if (gdata.coins >= 450 && !(gdata.balloon9 == 1) && stage == 3)
            {
                gdata.coins = gdata.coins - 450;
                gdata.balloon9 = 1;
            }
        });


        rve.visible = false;
    }

    private void updateShop()
    {
        if (stage == 1)
        {
            if (gdata.balloon1 == 1) { button1.text = "Owned"; }
            else { button1.text = "Costs 150 stars"; }
            if (gdata.balloon2 == 1) { button2.text = "Owned"; }
            else { button2.text = "Costs 200 stars"; }
            if (gdata.balloon3 == 1) { button3.text = "Owned"; }
            else { button3.text = "Costs 250 stars"; }
        }
        if (stage == 2)
        {
            if (gdata.balloon4 == 1) { button1.text = "Owned"; }
            else { button1.text = "Costs 150 stars"; }
            if (gdata.balloon5 == 1) { button2.text = "Owned"; }
            else { button2.text = "Costs 200 stars"; }
            if (gdata.balloon6 == 1) { button3.text = "Owned"; }
            else { button3.text = "Costs 350 stars"; }
        }
        if (stage == 3)
        {
            if (gdata.balloon7 == 1) { button1.text = "Owned"; }
            else { button1.text = "Costs 150 stars"; }
            if (gdata.balloon8 == 1) { button2.text = "Owned"; }
            else { button2.text = "Costs 200 stars"; }
            if (gdata.balloon9 == 1) { button3.text = "Owned"; }
            else { button3.text = "Costs 450 stars"; }
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
        turnoffimage();
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

    private void updateStage()
    {
        if (stage > gdata.stage)
        {
            gdata.stage = stage;
        }
    }

    private void Displayimage()
    {
        if (stage == 1)
        {
            skin1.visible = true;
            skin2.visible = true;
            skin3.visible = true;
        }
        if (stage == 2)
        {
            skin4.visible = true;
            skin5.visible = true;
            skin6.visible = true;
        }
        if (stage == 3)
        {
            skin7.visible = true;
            skin8.visible = true;
            skin9.visible = true;
        }
    }

    private void turnoffimage()
    {

        skin1.visible = false;
        skin2.visible = false;
        skin3.visible = false;
        skin4.visible = false;
        skin5.visible = false;
        skin6.visible = false;
        skin7.visible = false;
        skin8.visible = false;
        skin9.visible = false;

    }


}


// balloon.ChangeBalloonSkin("stripes-1");
