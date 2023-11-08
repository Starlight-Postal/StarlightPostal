using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreCounterBehaviour : MonoBehaviour
{
    private Label scoreLabel;

    private SaveFileManager save;

    private void OnEnable()
    {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        scoreLabel = rve.Q<Label>("score-counter-label");
    }

    private void Start()
    {
        save = FindObjectOfType<SaveFileManager>();
    }

    private void Update()
    {
        int score = save.saveData.score;
        scoreLabel.text = $"SCORE: {score:0000000}";
    }
}
