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
    }

    [Serializable]
    public class Preferences
    {

    }

    public SaveData saveData;
    //public Preferences preferences;

    private string saveFileName = "savedata";

    public void Save()
    {
        var form = new BinaryFormatter();
        var path = Application.persistentDataPath + "/" + saveFileName + ".dat";
        var fs = new FileStream(path, FileMode.Create);

        form.Serialize(fs, saveData);
        fs.Close();
    }

    public void Load()
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

    [ConsoleMethod("file.save", "")]
    public static void SaveFile()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().Save();
    }

    [ConsoleMethod("file.load", "")]
    public static void LoadFile()
    {
        GameObject.Find("Save File").GetComponent<SaveFileManager>().Load();
    }

}
