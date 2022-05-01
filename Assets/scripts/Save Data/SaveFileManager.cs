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
        public int coins = 0;
        public int checkpointId = 0;
        public string checkpointScene = "level 1";
        public bool introScene = true;
        public int stage = 0;
        public int equippedBalloon = 0;
        public bool[] balloonUnlock = {true, false, false, false, false, false, false, false, false, false, false};
    }

    [Serializable]
    public class Preferences
    {

    }

    public SaveData saveData;
    public Preferences preferences;

    private string saveFileName = "savedata";

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

    public bool SaveDataExists()
    {
        var path = Application.persistentDataPath + "/" + saveFileName + ".dat";
        return File.Exists(path);
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
            Debug.LogError("No prefs file found");
        }
    }
    
    public bool PreferencesExists()
    {
        var path = Application.persistentDataPath + "/playerprefs.dat";
        return File.Exists(path);
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

    [ConsoleMethod("data.coins", "Gets the players coin count")]
    public static void GetCoins()
    {
        var save = GameObject.FindObjectOfType<SaveFileManager>();
        Debug.Log("Coins: " + save.saveData.coins);
    }

    [ConsoleMethod("data.coins", "Sets the players coin count")]
    public static void SetCoins(int newCoinValue)
    {
        var save = GameObject.FindObjectOfType<SaveFileManager>();
        save.saveData.coins = newCoinValue;
        GetCoins();
    }

}
