using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject nameEntry;
    [SerializeField] private GameObject leaderboard;
    
    private void Start()
    {
        nameEntry.SetActive(true);
        leaderboard.SetActive(false);
    }

    public void ConfirmName()
    {
        // Record new score
        var nameEntryBehaviour = FindObjectOfType<LeaderboardNameEntryBehaviour>();
        if (nameEntryBehaviour == null)
            return;

        var save = FindObjectOfType<SaveFileManager>();
        if (save == null)
            return;

        string name = nameEntryBehaviour.GetEnteredName();
        int score = save.saveData.score;
            
        var data = FindObjectOfType<LeaderboardDataHandler>();
        if (data == null)
            return;
        
        data.AddLeaderboardEntry(new LeaderboardEntry(name, score));
        data.SaveLeaderboard();
        
        nameEntry.SetActive(false);
        leaderboard.SetActive(true);
    }
}
