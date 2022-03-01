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

        ChatButton.RegisterCallback<ClickEvent>(ev=> { nextLine(); });
        rve.visible = false;


    }

    private void nextLine()
    {
        Script.text = "change";
    }
}
