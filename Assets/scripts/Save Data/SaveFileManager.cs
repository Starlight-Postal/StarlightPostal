using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using IngameDebugConsole;

public class SaveFileManager : MonoBehaviour
{

    [Serializable]
    public class SaveData
    {
        public int coins;
        public int checkpointId;
        public string checkpointScene;
    }

    [Serializable]
    public class Preferences
    {

    }

    public SaveData saveData;
    public Preferences preferences;

    private string saveFileName = "savedata";

    void Start()
    {
        // Register instance commands
        DebugLogConsole.AddCommandInstance("data.coins", "Gets current coin count", "GetCoins", this);
        DebugLogConsole.AddCommandInstance("data.coins", "Sets current coin count", "SetCoins", this);
    }

    public void SaveSaveData()
    {
        var form = new BinaryFormatter();
        var path = Application.persistentDataPath + "/" + saveFileName + ".dat";
        var fs = new FileStream(path, FileMode.Create);

        form.Serialize(fs, saveData);
        fs.Close();
    }

    public void LoadSaveData()
    {
        var path = Application.persistentDataPath + "/" + saveFileName + ".dat";

        if (File.Exists(path))
        {
            var form = new BinaryFormatter();
            var fs = new FileStream(path, FileMode.Open);
            
            saveData = form.Deserialize(fs) as SaveData;
            fs.Close();
        } else
        {
            Debug.LogError("No save file found");
        }
    }

    public void SavePreferences()
    {
        var form = new BinaryFormatter();
        var path = Application.persistentDataPath + "/playerprefs.dat";
        var fs = new FileStream(path, FileMode.Create);

        form.Serialize(fs, preferences);
        fs.Close();
    }

    public void LoadPreferences()
    {
        var path = Application.persistentDataPath + "/playerprefs.dat";

        if (File.Exists(path))
        {
            var form = new BinaryFormatter();
            var fs = new FileStream(path, FileMode.Open);
            
            preferences = form.Deserialize(fs) as Preferences;
            fs.Close();
        } else
        {
            Debug.LogError("No save file found");
        }
    }

    [ConsoleMethod("file.save", "")]
    public static void SaveFile()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().SaveSaveData();
    }

    [ConsoleMethod("file.load", "")]
    public static void LoadFile()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().LoadSaveData();
    }

    [ConsoleMethod("prefs.save", "")]
    public static void SaveFilePref()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().SavePreferences();
    }

    [ConsoleMethod("prefs.load", "")]
    public static void LoadFilePref()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().LoadPreferences();
    }

    public void GetCoins()
    {
        Debug.Log("Coins: " + saveData.coins);
    }

    public void SetCoins(int newCoinValue)
    {
        saveData.coins = newCoinValue;
        GetCoins();
    }

}
