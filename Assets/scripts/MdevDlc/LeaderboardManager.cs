using System.Linq;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject nameEntry;
    [SerializeField] private GameObject leaderboard;
    
    private void Start()
    {
        var data = FindObjectOfType<LeaderboardDataHandler>();
        int score = FindObjectOfType<SaveFileManager>().saveData.score;
        if (score > data.GetTopLeaderboardEntriesSorted(10).Max(e => e.score))
        {
            nameEntry.SetActive(true);
            leaderboard.SetActive(false);
        }
        else
        {
            nameEntry.SetActive(false);
            leaderboard.SetActive(true);
        }
        
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
