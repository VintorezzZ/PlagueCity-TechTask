using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class DataManager
{
    private static string _directory = "/Config/";
    private static string _fileName = "data.txt";
    
    public static void Save()
    {
        string dir = Application.persistentDataPath + _directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        ObjectToSave ob = new ObjectToSave();
        ob.language = GameManager.Instance.playerInfo.Language;
        ob.screenResolutionWidth = GameManager.Instance.playerInfo.ScreenResolution.width;
        ob.screenResolutionHeight = GameManager.Instance.playerInfo.ScreenResolution.height;
        ob.music = GameManager.Instance.playerInfo.Music;
        ob.soundEffects = GameManager.Instance.playerInfo.SoundEffects;
        ob.visitors.AddRange(GameManager.Instance.playerInfo.currentVisitors);
        ob.dateTime = GameManager.Instance.playerInfo.currentDatetime.ToString();
        
        string json = JsonUtility.ToJson(ob);
        File.WriteAllText(dir + _fileName, json);
    }

    public static PlayerInfo Load()
    {
        string fullPath = Application.persistentDataPath + _directory + _fileName;
        ObjectToSave objectToSave = new ObjectToSave();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            objectToSave = JsonUtility.FromJson<ObjectToSave>(json);
        }

        PlayerInfo playerInfo = new PlayerInfo(objectToSave);
        
        return playerInfo;
    }
}