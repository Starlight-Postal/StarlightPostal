using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LivesHudBehaviour : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset lifeAsset;
    [SerializeField] private float lifeVisualWidth = 64.0f;

    private VisualElement livesContainer;
    private VisualElement[] lives;
    private SaveFileManager save;
    
    private void OnEnable()
    {
        save = FindObjectOfType<SaveFileManager>();
        var rve = GetComponent<UIDocument>().rootVisualElement;

        livesContainer = rve.Q<VisualElement>("lives-display-lives-container");

        int maxLives = new SaveFileManager.SaveData().lives;

        lives = new VisualElement[maxLives];
        
        for (int i = 0; i < maxLives; i++)
        {
            var lifeVe = lifeAsset.Instantiate();

            lifeVe.style.left = lifeVisualWidth * i;
            
            livesContainer.Add(lifeVe);
            lives[i] = lifeVe;
        }
    }

    private void Update()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].visible = save.saveData.lives > i;
        }
    }
}
