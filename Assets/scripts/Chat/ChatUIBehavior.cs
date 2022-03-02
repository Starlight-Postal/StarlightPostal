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

    void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            inMenu = !inMenu;
        }

        rve.visible = inMenu;

    }
    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        ChatButton = rve.Q<Button>("ChatButton");
        Script = rve.Q<Label>("Script");

        ChatButton.RegisterCallback<ClickEvent>(ev=> { nextLine(); counter++; });
        rve.visible = false;

    }

    private void nextLine()
    {
        if (counter == 1) { Script.text = "We are behind with they mail. You need to get to work right away"; }
        if (counter == 2) { Script.text = "Hurry, up and fly up to the first town with the mail"; }
    }
}
