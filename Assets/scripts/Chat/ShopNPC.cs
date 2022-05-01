using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ShopNPC : Interractable
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    public bool playerInRange;
    [SerializeField] public int stage;

    public bool inMenu = false;

    private Label Shop;
    private Label skin1;
    private Label skin2;
    private Label skin3;
    private Label skin4;
    private Label skin5;
    private Label skin6;
    private Label skin7;
    private Label skin8;
    private Label skin9;
    private Button Button1;
    private Button Button2;
    private Button Button3;

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
        updateStage();
        visualCue.SetActive(false);
    }

    private void OnEnable()
    {
        save = GameObject.FindObjectOfType<SaveFileManager>();
        rve = GetComponent<UIDocument>().rootVisualElement;

        Shop = rve.Q<Label>("Shop");

        skin1 = rve.Q<Label>("skin1");
        skin2 = rve.Q<Label>("skin2");
        skin3 = rve.Q<Label>("skin3");
        skin4 = rve.Q<Label>("skin4");
        skin5 = rve.Q<Label>("skin5");
        skin6 = rve.Q<Label>("skin6");
        skin7 = rve.Q<Label>("skin7");
        skin8 = rve.Q<Label>("skin8");
        skin9 = rve.Q<Label>("skin9");

        Button1 = rve.Q<Button>("Balloon1");
        Button2 = rve.Q<Button>("Balloon2");
        Button3 = rve.Q<Button>("Balloon3");

        Button1.RegisterCallback<ClickEvent>(ev =>
        {
            if (save.saveData.coins >= 150 && !(save.saveData.balloonUnlock[1]) && stage == 1)
            {
                save.saveData.coins = save.saveData.coins - 150;
                save.saveData.balloonUnlock[1] = true;
            }
            else if (save.saveData.coins >= 150 && !(save.saveData.balloonUnlock[4]) && stage == 2)
            {
                save.saveData.coins = save.saveData.coins - 150;
                save.saveData.balloonUnlock[4] = true;
            }
            else if (save.saveData.coins >= 150 && !(save.saveData.balloonUnlock[7]) && stage == 3)
            {
                save.saveData.coins = save.saveData.coins - 150;
                save.saveData.balloonUnlock[7] = true;
            }
        });
        Button2.RegisterCallback<ClickEvent>(ev =>
        {
            Debug.Log("end set up");

            if (save.saveData.coins >= 200 && !(save.saveData.balloonUnlock[2]) && stage == 1)
            {
                save.saveData.coins = save.saveData.coins - 200;
                save.saveData.balloonUnlock[2] = true;
            }
            else if (save.saveData.coins >= 200 && !(save.saveData.balloonUnlock[5]) && stage == 2)
            {
                save.saveData.coins = save.saveData.coins - 200;
                save.saveData.balloonUnlock[5] = true;
            }
            else if (save.saveData.coins >= 200 && !(save.saveData.balloonUnlock[8]) && stage == 3)
            {
                save.saveData.coins = save.saveData.coins - 200;
                save.saveData.balloonUnlock[8] = true;
            }
        });
        Button3.RegisterCallback<ClickEvent>(ev =>
        {
            Debug.Log("end set up");

            if (save.saveData.coins >= 250 && !(save.saveData.balloonUnlock[3]) && stage == 1)
            {
                save.saveData.coins = save.saveData.coins - 250;
                save.saveData.balloonUnlock[3] = true;
            }
            else if (save.saveData.coins >= 350 && !(save.saveData.balloonUnlock[6]) && stage == 2)
            {
                save.saveData.coins = save.saveData.coins - 250;
                save.saveData.balloonUnlock[6] = true;
            }
            else if (save.saveData.coins >= 450 && !(save.saveData.balloonUnlock[9]) && stage == 3)
            {
                save.saveData.coins = save.saveData.coins - 250;
                save.saveData.balloonUnlock[9] = true;
            }
        });


        rve.visible = false;
        Debug.Log("end set up");

    }

    private void updateShop()
    {
        if (stage == 1)
        {
            if (save.saveData.balloonUnlock[1]) { Button1.text = "Owned"; }
            else { Button1.text = "Costs 150 stars"; }
            if (save.saveData.balloonUnlock[2]) { Button2.text = "Owned"; }
            else { Button2.text = "Costs 200 stars"; }
            if (save.saveData.balloonUnlock[3]) { Button3.text = "Owned"; }
            else { Button3.text = "Costs 250 stars"; }
        }
        if (stage == 2)
        {
            if (save.saveData.balloonUnlock[4]) { Button1.text = "Owned"; }
            else { Button1.text = "Costs 150 stars"; }
            if (save.saveData.balloonUnlock[5]) { Button2.text = "Owned"; }
            else { Button2.text = "Costs 200 stars"; }
            if (save.saveData.balloonUnlock[6]) { Button3.text = "Owned"; }
            else { Button3.text = "Costs 250 stars"; }
        }
        if (stage == 3)
        {
            if (save.saveData.balloonUnlock[7]) { Button1.text = "Owned"; }
            else { Button1.text = "Costs 150 stars"; }
            if (save.saveData.balloonUnlock[8]) { Button2.text = "Owned"; }
            else { Button2.text = "Costs 200 stars"; }
            if (save.saveData.balloonUnlock[9]) { Button3.text = "Owned"; }
            else { Button3.text = "Costs 250 stars"; }
        }
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
        turnoffimage();
        rve.visible = false;
    }

    private void openShop()
    {
        rve.visible = true;
        updateShop();
    }

    private void updateStage()
    {
        if (stage > save.saveData.stage)
        {
            save.saveData.stage = stage;
        }
        turnoffimage();
        Displayimage();
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


