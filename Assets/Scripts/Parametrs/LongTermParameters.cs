using System;
using System.IO;
using UnityEngine;

public class LongTermParameters
{
    public const string KEY = "LongTermParameters";

    public string Name = "";
    public string Birthday = "";
    public bool FirstEntry = true;
    public int Sex = 0;

    public string TimeLastViewedAd = "11.06.2001";

    public PredictionForZodiacs[] Predications = new PredictionForZodiacs[12]
    {
        new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} }, new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },
        new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} }, new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },
        new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} }, new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },
        new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} }, new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} },new PredictionForZodiacs{ IndexsPredication = new int[3] { 0, 0, 0} }
    };

    public void Save(LongTermParameters longTermParameters)
    {
        string json = JsonUtility.ToJson(longTermParameters);
        PlayerPrefs.SetString(KEY, json);
    }

    private string savePath;
    private string saveFileName = "data.json";

    public void SaveToFile()
    {
        GameCoreStruct gameCore = new GameCoreStruct
        {
            Name = this.Name,
            Birthday = this.Birthday,
            FirstEntry = this.FirstEntry,
            Sex = this.Sex,
            TimeLastViewedAd = this.TimeLastViewedAd,
            Predications = this.Predications
        };

        string json = JsonUtility.ToJson(gameCore, true);

        try
        {
            File.WriteAllText(savePath, json);
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
        }
    }

    public void LoadFromFile()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif

        Debug.Log("savePAth: " + savePath);

        if (!File.Exists(savePath))
        {
            Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
            return;
        }

        try
        {
            string json = File.ReadAllText(savePath);

            GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

            this.Name = gameCoreFromJson.Name;
            this.Birthday = gameCoreFromJson.Birthday;
            this.FirstEntry = gameCoreFromJson.FirstEntry;
            this.Sex = gameCoreFromJson.Sex;
            this.TimeLastViewedAd = gameCoreFromJson.TimeLastViewedAd;
            this.Predications = gameCoreFromJson.Predications;
        }
        catch (Exception e)
        {
            Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
        }
    }
}
