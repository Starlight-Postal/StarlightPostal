using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class BalloonEquipter : Interractable
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;
    [SerializeField] public int stage;

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
    private Button Button10;

    [SerializeField] private VisualElement rve;

    public global_data gdata;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        updateShop();
    }

    private void Update()
    {
        if (inMenu) { openShop(); }
        else { closeShop(); }
    }

    public override void OnPlayerInterract()
    {
        inMenu = true;
        updateButtons();
        visualCue.SetActive(false);
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
        Button10 = rve.Q<Button>("egg");

        Button0.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon0 == 1 && !(gdata.balloon == 0))
            {
                Label0.text = "Equipped";
                balloon.ChangeBalloonSkin("logo-1");
                gdata.balloon = 0;
            }
        });
        Button1.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon1 == 1 && !(gdata.balloon == 1))
            {
                Label1.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_01");
                gdata.balloon = 1;
            }
        });
        Button2.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon2 == 1 && !(gdata.balloon == 2))
            {
                Label2.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_01");
                gdata.balloon = 2;
            }
        });
        Button3.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon3 == 1 && !(gdata.balloon == 3))
            {
                Label3.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_01");
                gdata.balloon = 3;
            }
        });
        Button4.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon4 == 1 && !(gdata.balloon == 4))
            {
                Label4.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_02");
                gdata.balloon = 4;
            }
        });
        Button5.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon5 == 1 && !(gdata.balloon == 5))
            {
                Label5.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_02");
                gdata.balloon = 5;
            }
        });
        Button6.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon6 == 1 && !(gdata.balloon == 6))
            {
                Label6.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_02");
                gdata.balloon = 6;
            }
        });
        Button7.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon7 == 1 && !(gdata.balloon == 7))
            {
                Label7.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_03");
                gdata.balloon = 7;
            }
        });
        Button8.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon8 == 1 && !(gdata.balloon == 8))
            {
                Label8.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_03");
                gdata.balloon = 8;
            }
        });
        Button9.RegisterCallback<ClickEvent>(ev =>
        {
        
            if (gdata.balloon9 == 1 && !(gdata.balloon == 9))
            {
                Label9.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_03");
                gdata.balloon = 9;
            }
        });
        Button10.RegisterCallback<ClickEvent>(ev =>
        {
            if (gdata.balloon == 10)
            {
                balloon.ChangeBalloonSkin("stripes-1");
                gdata.balloon = 11;
            }
            else
            {
                balloon.ChangeBalloonSkin("epic");
                gdata.balloon = 10;
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
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
            inMenu = false;
        }
    }

    private void closeShop()
    {
        rve.visible = false;
        Button10.visible = false;
    }

    private void openShop()//asdf
    {
        rve.visible = true;
        updateShop();
        Button10.visible = true;

    }

    private void updateButtons()
    {
        if (gdata.stage >= 1)
        {
            Button1.text = "Red balloon";
            Button2.text = "Maple Leaf";
            Button3.text = "Train";
        }
        if (gdata.stage >= 2)
        {
            Button4.text = "Blue Zig-Zag";
            Button5.text = "Mushroom";
            Button6.text = "Vampire";
        }
        if (gdata.stage == 3)
        {
            Button7.text = "Blue Waves";
            Button8.text = "Snowflake";
            Button9.text = "Mitten";
        }
    }
}