using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LeaderboardUIBehaviour : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset entryAsset;
    [SerializeField] private float minimumVisibleTime = 2.0f;
    [SerializeField] private float leaderboardTimeoutTime = 10.0f;

    
    private VisualElement entryList;
    private LeaderboardDataHandler data;
    private bool minimumTimeInputLockoutPassed = false;

    private void OnEnable()
    {
        var rve = GetComponent<UIDocument>().rootVisualElement;

        entryList = rve.Q<VisualElement>("leaderboard-entry-list");
        
        data = FindObjectOfType<LeaderboardDataHandler>();
        data.onLeaderboardUpdated += RepopulateLeaderboard;

        RepopulateLeaderboard();
        StartCoroutine(LeaderboardTimeoutCorroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void OnAnyJoystickButtonDown()
    {
        if (minimumTimeInputLockoutPassed)
            ProceedToMainMenu();
    }

    public void ProceedToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator LeaderboardTimeoutCorroutine()
    {
        yield return new WaitForSecondsRealtime(minimumVisibleTime);
        minimumTimeInputLockoutPassed = true;
        yield return new WaitForSecondsRealtime(leaderboardTimeoutTime - minimumVisibleTime);
        ProceedToMainMenu();
    }

    private void RepopulateLeaderboard()
    {
        entryList.hierarchy.Clear();
        
        List<LeaderboardEntry> entries = data.GetTopLeaderboardEntriesSorted(10);

        for (int i = 0; i < Math.Min(entries.Count, 10); i++)
        {
            if (entries[i].score < 0)
                continue;
            
            VisualElement entry = entryAsset.Instantiate();
            entryList.Add(entry);

            Label nameLabel = entry.Q<Label>("leaderboard-entry-name");
            Label scoreLabel = entry.Q<Label>("leaderboard-entry-score");
            
            nameLabel.text = $"{CalculateRankString(i+1)} {entries[i].name}";
            scoreLabel.text = $"{entries[i].score:0000000}";
        }
    }

    private string CalculateRankString(int i)
    {
        string extraSpaces = "";
        if (i < 10)
        {
            extraSpaces = " ";
        }

        string suffix;
        switch (i)
        {
            case 1:
                suffix = "st";
                break;
            case 2:
                suffix = "nd";
                break;
            case 3:
                suffix = "rd";
                break;
            default:
                suffix = "th";
                break;
        }
        
        return $"{extraSpaces}{i}{suffix}";
    }
}
