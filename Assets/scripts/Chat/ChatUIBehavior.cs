using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChatUIBehavior : MonoBehaviour
{

    public bool inMenu = false;

    // Start is called before the first frame update
    private Button ChatButton;
    private Label Script;

    private VisualElement rve;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    private bool playerInRange;


    void Update()
    {
       if (Input.GetKeyDown("i"))
        {
             inMenu = !inMenu;
        }

        if (playerInRange)
        {
            // dialoguePanel.SetActive(true);
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
            if (rve.visible == true && visualCue == false)
            {
                inMenu = !inMenu;
            }
        }

        rve.visible = inMenu;

    }
    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        ChatButton = rve.Q<Button>("ChatButton");
        Script = rve.Q<Label>("Script");

        ChatButton.RegisterCallback<ClickEvent>(ev=> { nextLine(); });
        rve.visible = false;
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void nextLine()
    {
        Script.text = "change";
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
