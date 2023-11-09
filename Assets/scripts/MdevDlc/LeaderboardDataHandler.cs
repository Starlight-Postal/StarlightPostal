using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public struct LeaderboardEntry
{
    public string name;
    public int score;

    public LeaderboardEntry(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class LeaderboardDataHandler : MonoBehaviour
{
    public delegate void OnLeaderboardUpdated();
    public OnLeaderboardUpdated onLeaderboardUpdated;
    
    private List<LeaderboardEntry> leaderboard;

    private void OnDestroy()
    {
        SaveLeaderboard();
    }

    public List<LeaderboardEntry> GetLeaderboardEntries()
    {
        if (leaderboard != null)
            return leaderboard;
        
        LoadLeaderboard();

        return leaderboard;
    }

    public void AddLeaderboardEntry(LeaderboardEntry entry)
    {
        leaderboard.Add(entry);
    }

    public void SaveLeaderboard()
    {
        if (leaderboard == null)
        {
            Debug.LogError("Cannot save a null leaderboard!");
            return;
        }
        
        var formatter = new BinaryFormatter();
        string path = $"{Application.persistentDataPath}/leaderboard.dat";
        var stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, leaderboard);
        stream.Close();
    }

    public void LoadLeaderboard()
    {
        string path = $"{Application.persistentDataPath}/leaderboard.dat";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            try
            {
                leaderboard = formatter.Deserialize(stream) as List<LeaderboardEntry>;
            }
            catch (SerializationException e)
            {
                Debug.LogError($"Error deserializing leaderboard: {e}");
                ClearLeaderboard();
            }
            finally
            {
                stream.Close();
            }
        }
        else
        {
            Debug.LogError("No leaderboard file found!");
            ClearLeaderboard();
        }

        onLeaderboardUpdated();
    }

    public void ClearLeaderboard()
    {
        leaderboard = new List<LeaderboardEntry>();
        onLeaderboardUpdated();
    }

    public List<LeaderboardEntry> GetTopLeaderboardEntriesSorted(int num)
    {
        var topEntries = Enumerable.Range(0, num).Select(x => new LeaderboardEntry("", Int32.MinValue)).ToList();

        int spotsClaimed = 0;

        foreach (var entry in GetLeaderboardEntries())
        {
            if (spotsClaimed == 0)
            {
                topEntries[0] = entry;
                spotsClaimed++;
                continue;
            }
            
            for (int i = 0; i < Math.Min(topEntries.Count, spotsClaimed); i++)
            {
                if (entry.score >= topEntries[i].score)
                {
                    topEntries[i] = entry;
                    spotsClaimed++;
                    break;
                }
            }
        }

        return topEntries.OrderByDescending(e => e.score).ToList();
    }
}
