using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FontSizifier : MonoBehaviour {

    public float fontSizePercent = 10;
    
    private VisualElement rve;

    void OnEnable() {
        rve = GetComponent<UIDocument>().rootVisualElement;
        rve.RegisterCallback<GeometryChangedEvent>(ev => { Rescale(); });

        Rescale();
        
        #if PLATFORM_ANDROID
        fontSizePercent /= 2;
        if ((float) Screen.width / (float) Screen.height >= 2)
        {
            fontSizePercent /= 2;
        }
        #endif
    }

    private void Rescale() {
        int vh = Screen.height;
        var labels = rve.Query<Label>();
        labels.ForEach(l => {
            l.style.fontSize = (fontSizePercent / 100) * (float) vh;
        });
        var buttons = rve.Query<Button>();
        buttons.ForEach(b => {
            b.style.fontSize = (fontSizePercent / 100) * (float) vh;
        });
    }

}
