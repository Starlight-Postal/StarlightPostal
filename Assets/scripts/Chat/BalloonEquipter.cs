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

    private SaveFileManager save;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
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
        save = GameObject.FindObjectOfType<SaveFileManager>();
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
            if (save.saveData.balloonUnlock[0] && !(save.saveData.equippedBalloon == 0))
            {
                Label0.text = "Equipped";
                balloon.ChangeBalloonSkin("stripes-1");
                save.saveData.equippedBalloon = 0;
            }
        });
        Button1.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[1] && !(save.saveData.equippedBalloon == 1))
            {
                Label1.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_01");
                save.saveData.equippedBalloon = 1;
            }
        });
        Button2.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[2] && !(save.saveData.equippedBalloon == 2))
            {
                Label2.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_01");
                save.saveData.equippedBalloon = 2;
            }
        });
        Button3.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[3] && !(save.saveData.equippedBalloon == 3))
            {
                Label3.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_01");
                save.saveData.equippedBalloon = 3;
            }
        });
        Button4.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[4] && !(save.saveData.equippedBalloon == 4))
            {
                Label4.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_02");
                save.saveData.equippedBalloon = 4;
            }
        });
        Button5.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[5] && !(save.saveData.equippedBalloon == 5))
            {
                Label5.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_02");
                save.saveData.equippedBalloon = 5;
            }
        });
        Button6.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[6] && !(save.saveData.equippedBalloon == 6))
            {
                Label6.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_02");
                save.saveData.equippedBalloon = 6;
            }
        });
        Button7.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[7] && !(save.saveData.equippedBalloon == 7))
            {
                Label7.text = "Equipped";
                balloon.ChangeBalloonSkin("Skin_pattern_03");
                save.saveData.equippedBalloon = 7;
            }
        });
        Button8.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.balloonUnlock[8] && !(save.saveData.equippedBalloon == 8))
            {
                Label8.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_image_03");
                save.saveData.equippedBalloon = 8;
            }
        });
        Button9.RegisterCallback<ClickEvent>(ev =>
        {
        
            if (save.saveData.balloonUnlock[9] && !(save.saveData.equippedBalloon == 9))
            {
                Label9.text = "Equipped";
                balloon.ChangeBalloonSkin("skin_shape_03");
                save.saveData.equippedBalloon = 9;
            }
        });
        Button10.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.equippedBalloon == 10)
            {
                balloon.ChangeBalloonSkin("logo-1");
                save.saveData.equippedBalloon = 11;
            }
            else if(save.saveData.equippedBalloon == 11)
            {
                balloon.ChangeBalloonSkin("Cool_Balloonie");
                save.saveData.equippedBalloon = 12;
            }
            else
            {
                balloon.ChangeBalloonSkin("epic");
                save.saveData.equippedBalloon = 10;
            }
        });
       
        rve.visible = false;
    }

    private void updateShop()
    {
        if (save.saveData.balloonUnlock[0] && !(save.saveData.equippedBalloon == 0)) { Label0.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[1] && !(save.saveData.equippedBalloon == 1)) { Label1.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[2] && !(save.saveData.equippedBalloon == 2)) { Label2.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[3] && !(save.saveData.equippedBalloon == 3)) { Label3.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[4] && !(save.saveData.equippedBalloon == 4)) { Label4.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[5] && !(save.saveData.equippedBalloon == 5)) { Label5.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[6] && !(save.saveData.equippedBalloon == 6)) { Label6.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[7] && !(save.saveData.equippedBalloon == 7)) { Label7.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[8] && !(save.saveData.equippedBalloon == 8)) { Label8.text = "Can Equip"; }
        if (save.saveData.balloonUnlock[9] && !(save.saveData.equippedBalloon == 9)) { Label9.text = "Can Equip"; }
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
        if (save.saveData.stage >= 1)
        {
            Button1.text = "Sunset";
            Button2.text = "Sycamore";
            Button3.text = "Steamtrain";
        }
        if (save.saveData.stage >= 2)
        {
            Button4.text = "Spelunker";
            Button5.text = "Toadstool";
            Button6.text = "Vesper";
        }
        if (save.saveData.stage == 3)
        {
            Button7.text = "Aurora";
            Button8.text = "Blizzard";
            Button9.text = "Mitten";
        }
    }
}