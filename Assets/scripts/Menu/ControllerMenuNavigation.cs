using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class ControllerMenuNavigation : MonoBehaviour {

    private List<Button> buttons = new List<Button>();
    public int selectionIndex = 0;
    private bool usingNav;

    void OnEnable() {
        var rve = GetComponent<UIDocument>().rootVisualElement;
        var buttonsQuery = rve.Query<Button>();
        buttonsQuery.ForEach(btn => {
            Debug.Log("button");
            buttons.Add(btn);
        });
        Debug.Log(buttons.Count);
    }

    void OnNavigate(float v) {
        Debug.Log(v);
        if (Mathf.Abs(v) > 0.85f) {
            var prevIndex = selectionIndex;
            selectionIndex += v < 0 ? 1 : -1;
            usingNav = true;
            selectionIndex = selectionIndex % buttons.Count;

            buttons[selectionIndex].EnableInClassList("sp-controller-selection", true);
            buttons[prevIndex].EnableInClassList("sp-controller-selection", false);
        }
    }

    void OnNavigateUp() {
        OnNavigate(1);
    }

    void OnNavigateDown() {
        OnNavigate(-1);
    }

    void OnSelect() {
        buttons[selectionIndex].ExecuteDefaultAction(new ClickEvent());
    }

}
