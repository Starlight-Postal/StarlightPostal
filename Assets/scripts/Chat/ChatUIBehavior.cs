using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ChatUIBehavior : MonoBehaviour
{

    public bool inMenu = false;

    // Start is called before the first frame update
    private Button ChatButton;
    private Label Script;
    private int counter;

    private VisualElement rve;
    public bool playerInRange;


    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inMenu = !inMenu;
            counter = 0;
        }
        rve.visible = inMenu;
    }
    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        ChatButton = rve.Q<Button>("ChatButton");
        Script = rve.Q<Label>("chatLabel");

        ChatButton.RegisterCallback<ClickEvent>(ev=> { counter++; nextLine();} );
        rve.visible = false;

    }

    private void nextLine()
    {
       
        if (counter == 1) { Script.text = "I hate to say this, but we are running behind on mail"; }
        else if (counter == 2) { Script.text = "we would like for you to start right away";}
        else if (counter == 3) { Script.text = "Hurry, up and fly up to the first town with the mail"; }
        else { rve.visible = false; counter = 0; }
    }
}
