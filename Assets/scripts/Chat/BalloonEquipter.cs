using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class BalloonEquipter : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;
    [SerializeField] public int stage;


    public player player;
    public balloon balloon;
    public anchor anchor;


    public bool inMenu = false;


    private Label Shop;
    private Label Label0;
    private Label Label1;
    private Label Label2;
    private Label Label3;
    private Label Label4;
    private Label Label5;
    private Label Label6;
    private Label Label7;
    private Label Label8;
    private Label Label9;
    private Button Button0;
    private Button Button1;
    private Button Button2;
    private Button Button3;
    private Button Button4;
    private Button Button5;
    private Button Button6;
    private Button Button7;
    private Button Button8;
    private Button Button9;

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
                updateButtons();
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

        Label0 = rve.Q<Label>("Label0");
        Label1 = rve.Q<Label>("Label1");
        Label2 = rve.Q<Label>("Label2");
        Label3 = rve.Q<Label>("Label3");
        Label4 = rve.Q<Label>("Label4");
        Label5 = rve.Q<Label>("Label5");
        Label6 = rve.Q<Label>("Label6");
        Label7 = rve.Q<Label>("Label7");
        Label8 = rve.Q<Label>("Label8");
        Label9 = rve.Q<Label>("Label9");

        Button0 = rve.Q<Button>("Button0");
        Button1 = rve.Q<Button>("Button1");
        Button2 = rve.Q<Button>("Button2");
        Button3 = rve.Q<Button>("Button3");
        Button4 = rve.Q<Button>("Button4");
        Button5 = rve.Q<Button>("Button5");
        Button6 = rve.Q<Button>("Button6");
        Button7 = rve.Q<Button>("Button7");
        Button8 = rve.Q<Button>("Button8");
        Button9 = rve.Q<Button>("Button9");

        Button0.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon0 == 1)
            {
                Label0.text = "Equipped";
                balloon.ChangeBalloonSkin("logo-1");
                gdata.balloon = 0;
            }
        });
        Button1.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon1 == 1)
            {
                Label1.text = "Equipped";
                balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 1;
            }
        });
        Button2.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon2 == 1)
            {
                Label2.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 2;
            }
        });
        Button3.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon3 == 1)
            {
                Label3.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 3;
            }
        });
        Button4.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon4 == 1)
            {
                Label4.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 4;
            }
        });
        Button5.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon5 == 1)
            {
                Label5.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 5;
            }
        });
        Button6.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon6 == 1)
            {
                Label6.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 6;
            }
        });
        Button7.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon7 == 1)
            {
                Label7.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 7;
            }
        });
        Button8.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon8 == 1)
            {
                Label8.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 8;
            }
        });
        Button9.RegisterCallback<ClickEvent>(ev =>
        {
        
            if (gdata.balloon9 == 1)
            {
                Label9.text = "Equipped";
                // balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 9;
            }
        });



        rve.visible = false;
    }

    private void updateShop()
    {
        if (gdata.balloon0 == 1 && !(gdata.balloon == 0)) { Label0.text = "Can Equip"; }
        if (gdata.balloon1 == 1 && !(gdata.balloon == 1)) { Label1.text = "Can Equip"; }
        if (gdata.balloon2 == 1 && !(gdata.balloon == 2)) { Label2.text = "Can Equip"; }
        if (gdata.balloon3 == 1 && !(gdata.balloon == 3)) { Label3.text = "Can Equip"; }
        if (gdata.balloon4 == 1 && !(gdata.balloon == 4)) { Label4.text = "Can Equip"; }
        if (gdata.balloon5 == 1 && !(gdata.balloon == 5)) { Label5.text = "Can Equip"; }
        if (gdata.balloon6 == 1 && !(gdata.balloon == 6)) { Label6.text = "Can Equip"; }
        if (gdata.balloon7 == 1 && !(gdata.balloon == 7)) { Label7.text = "Can Equip"; }
        if (gdata.balloon8 == 1 && !(gdata.balloon == 8)) { Label8.text = "Can Equip"; }
        if (gdata.balloon9 == 1 && !(gdata.balloon == 9)) { Label9.text = "Can Equip"; }
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
        rve.visible = true;
        updateShop();
    }

    private void updateButtons()
    {
        if (gdata.stage >= 2)
        {
            Button4.text = "";
            Button5.text = "";
            Button6.text = "";
        }
        if (gdata.stage == 3)
        {
            Button7.text = "";
            Button8.text = "";
            Button9.text = "";
        }
    }
}



// balloon.ChangeBalloonSkin("stripes-1");