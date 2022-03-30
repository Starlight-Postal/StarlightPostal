using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarCounterHudController : MonoBehaviour {

    private VisualElement rve;
    private Label label;

    private global_data gd;

    private void OnEnable() {
        rve = GetComponent<UIDocument>().rootVisualElement;
        label = rve.Q<Label>("star-counter-label");
        gd = GameObject.Find("Globals").GetComponent<global_data>();
    }

    private void FixedUpdate() {
        label.text = gd.coins.ToString();
    }

}
