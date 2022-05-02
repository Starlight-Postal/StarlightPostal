using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarCounterHudController : MonoBehaviour {

    private VisualElement rve;
    private Label label;

    private SaveFileManager gd;

    private void OnEnable() {
        rve = GetComponent<UIDocument>().rootVisualElement;
        label = rve.Q<Label>("star-counter-label");
        gd = GameObject.FindObjectsOfType<SaveFileManager>()[0];
    }

    private void FixedUpdate() {
        label.text = gd.saveData.coins.ToString();
    }

}
