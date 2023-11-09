using System;
using System.Collections;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderboardNameEntryBehaviour : MonoBehaviour
{
    [SerializeField] private int selectedReel;
    [SerializeField] private char[] selectedLetters = { 'A', 'A', 'A' };

    private VisualElement[] letters = { null, null, null };

    private void OnEnable()
    {
        FindObjectOfType<JoystickInputHandler>().SetInMenu(true);

        var rve = GetComponent<UIDocument>().rootVisualElement;

        letters[0] = rve.Q<VisualElement>("keyboard-letter-0");
        letters[1] = rve.Q<VisualElement>("keyboard-letter-1");
        letters[2] = rve.Q<VisualElement>("keyboard-letter-2");

        UpdateUi();
    }

    private void OnDestroy()
    {
        FindObjectOfType<JoystickInputHandler>().SetInMenu(false);
    }

    void OnUiRight()
    {
        selectedReel = (selectedReel + 1) % 3;
        
        UpdateUi();
    }

    void OnUiLeft()
    {
        selectedReel = (selectedReel - 1) % 3;
        if (selectedReel < 0)
        {
            selectedReel = 2;
        }
        
        UpdateUi();
    }

    void OnUiUp()
    {
        int letter = (((selectedLetters[selectedReel] - 'A') + 1) % ('Z' - 'A' + 1)) + 'A';
        selectedLetters[selectedReel] = (char)letter;
        
        UpdateUi();
    }

    void OnUiDown()
    {
        int letter = (((selectedLetters[selectedReel] - 'A') - 1) % ('Z' - 'A' + 1)) + 'A';
        selectedLetters[selectedReel] = (char)letter;
        if (selectedLetters[selectedReel] == 'A' - 1)
        {
            selectedLetters[selectedReel] = 'Z';
        }
        
        UpdateUi();
    }

    void OnUiSelect()
    {
        if (selectedReel >= 2)
        {
            ConfirmName();
            
            return;
        }

        OnUiRight();
    }

    private void ConfirmName()
    {
        Debug.Log($"Entered name is {GetEnteredName()}");
        FindObjectOfType<LeaderboardManager>().ConfirmName();
    }

    public string GetEnteredName()
    {
        return new string(selectedLetters);
    }

    private void UpdateUi()
    {
        for (int i = 0; i < 3; i++)
        {
            // Update selection
            letters[i].Q<VisualElement>("letter-selector").visible = selectedReel == i;
            
            // Update selected letter
            letters[i].Q<Label>("letter-label").text = $"{selectedLetters[i]}";
            
            // Update previous letter
            char prev = (char)((selectedLetters[i] - 'A' - 1) % ('Z' - 'A' + 1) + 'A');
            if (prev == 'A' - 1)
            {
                prev = 'Z';
            }
            letters[i].Q<Label>("letter-label-down").text = $"{prev}";
            
            // Update next letter
            char next = (char)((selectedLetters[i] - 'A' + 1) % ('Z' - 'A' + 1) + 'A');
            letters[i].Q<Label>("letter-label-up").text = $"{next}";
        }
    }
}
