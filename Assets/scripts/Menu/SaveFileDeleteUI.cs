using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;

public class SaveFileDeleteUI : MonoBehaviour
{
    
    private const float CONT_APSPECT_RATIO = 0.8f;

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
        
        rve.RegisterCallback<GeometryChangedEvent>(ev => { Rescale(); });

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
    
    private void Rescale() {
        Debug.Log("Resizing delete menu UI to new screen size");
        
        float contWidth;
        float contHeight;
        
        if (Screen.width <= Screen.height) {
            contWidth = Screen.width * 1.0f;
            contHeight = contWidth * (1f / CONT_APSPECT_RATIO);
        } else {
            contHeight = Screen.height * 0.75f;
            contWidth = contHeight * CONT_APSPECT_RATIO;
        }

#if PLATFORM_ANDROID
        contHeight /= 2;
        contWidth /= 2;
        if ((float) Screen.width / (float) Screen.height >= 2)
        {
            contHeight /= 2;
            contWidth /= 2;
        }
#endif
            
        container.style.height = contHeight;
        container.style.width = contWidth;
    }
    
}
