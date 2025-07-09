using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string currPlayerName, PlayerHighScoreName;
    public int PlayerHighScore;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScoreData();
    }

    public void SaveHighScoreData()
    {
        SaveData data = new SaveData();
        data.PlayerHighScoreName = PlayerHighScoreName;
        data.PlayerHighScore = PlayerHighScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScoreData()
    {
        PlayerHighScoreName = null;
        string path = Application.persistentDataPath + "/highscore.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PlayerHighScoreName = data.PlayerHighScoreName;
            PlayerHighScore = data.PlayerHighScore;
        }
    }
}

[System.Serializable]
class SaveData
{
    public string PlayerHighScoreName;
    public int PlayerHighScore;
}
