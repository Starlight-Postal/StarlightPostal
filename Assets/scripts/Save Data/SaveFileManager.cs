using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using IngameDebugConsole;
using Random = System.Random;

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
        public float[] heightCaps = {60, 215, 180};
    }

    [Serializable]
    public class Preferences
    {
        public string saveFileName = "savedata";
        public int devId = GenerateDeviceId();
        public float volSfx = 0.0f;
        public float volMusic = 0.0f;
        public float volEnv = 0.0f;
    }

    public SaveData saveData;
    public Preferences preferences;

    private static string deviceId;

    private void Start()
    {
        deviceId = SystemInfo.deviceUniqueIdentifier;
        preferences = new Preferences();
    }

    public void SaveSaveData()
    {
        var form = new BinaryFormatter();
        var path = Application.persistentDataPath + "/" + preferences.saveFileName + ".dat";
        var fs = new FileStream(path, FileMode.Create);

        form.Serialize(fs, saveData);
        fs.Close();
    }

    public void LoadSaveData()
    {
        var path = Application.persistentDataPath + "/" + preferences.saveFileName + ".dat";

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
        var path = Application.persistentDataPath + "/" + preferences.saveFileName + ".dat";
        return File.Exists(path);
    }

    public void DeleteSave()
    {
        var path = Application.persistentDataPath + "/" + preferences.saveFileName + ".dat";
        File.Delete(path);
        saveData = new SaveData();
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

    public void DeletePrefs()
    {
        var path = Application.persistentDataPath + "/playerprefs.dat";
        File.Delete(path);
        preferences = new Preferences();
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
    
    private static int GenerateDeviceId()
    {
        int hash = 0;
        Debug.Log("ID:" + deviceId);
        if (deviceId == SystemInfo.unsupportedIdentifier)
        {
            hash = new Random().Next();
        }
        else
        {
            for (int i = 0; i < deviceId.Length; i++)
            {
                hash += deviceId[i];
            }
        }

        return hash;
    }

}
