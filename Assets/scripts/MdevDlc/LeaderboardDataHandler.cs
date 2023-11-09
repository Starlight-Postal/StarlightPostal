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
        try
        {
            LoadLeaderboard();
        }
        catch (Exception e)
        {
            return new List<LeaderboardEntry>();
        }

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
        try
        {
            onLeaderboardUpdated();
        }
        catch (Exception e)
        {
            Debug.Log("Error sending leaderboard update");
        }
    }

    public List<LeaderboardEntry> GetTopLeaderboardEntriesSorted(int num)
    {
        var ordered = GetLeaderboardEntries().OrderByDescending(e => e.score).ToList();
        return ordered.GetRange(0, Math.Min(num, ordered.Count));
    }
}
