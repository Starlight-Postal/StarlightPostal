using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;

public class SaveFileDeleteUI : MonoBehaviour
{

    private VisualElement rve;
    private VisualElement container;

    private Button yesButton;
    private Button noButton;
    private Toggle prefsToggle;

    private SaveFileManager save;

    private void OnEnable()
    {
        rve = GetComponent<UIDocument>().rootVisualElement;
        
        container = rve.Q<VisualElement>("options-menu-container");

        yesButton = rve.Q<Button>("yes-button");
        noButton = rve.Q<Button>("no-button");
        prefsToggle = rve.Q<Toggle>("prefs-toggle");
        
        yesButton.RegisterCallback<ClickEvent>(ev => { OnYesClick(); });
        noButton.RegisterCallback<ClickEvent>(ev => { OnNoClick(); });

        save = GameObject.FindObjectOfType<SaveFileManager>();

        rve.visible = false;
    }

    public void ConfirmDelete()
    {
        rve.visible = true;
    }

    private void OnYesClick()
    {
        save.DeleteSave();
        if (prefsToggle.value)
        {
            save.DeletePrefs();
        }
        GameObject.FindObjectOfType<OptionsMenuBehaviour>().ShowMenu();
        rve.visible = false;
    }

    private void OnNoClick()
    {
        rve.visible = false;
    }
    
}
