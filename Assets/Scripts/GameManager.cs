using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string currPlayerName, PlayerHighScoreName;
    public int PlayerHighScore;
    public List<PlayerScore> playerScores;

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
        LoadPlayerScores();
    }

    public void AddPlayerScore(int score)
    {
        if (playerScores == null)
            playerScores = new List<PlayerScore>();

        var existingPlayer = playerScores.Find(p => p.Name == Instance.currPlayerName);
        if (existingPlayer != null)
        {
            existingPlayer.Score = score;
        }
        else
        {
            playerScores.Add(new PlayerScore { Name = Instance.currPlayerName, Score = score });
        }
    }
    public void SavePlayerScores()
    {
        PlayerScoresData data = new PlayerScoresData();
        data.playerScores = playerScores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerscores.json", json);
    }

    public void LoadPlayerScores()
    {
        string path = Application.persistentDataPath + "/playerscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerScoresData data = JsonUtility.FromJson<PlayerScoresData>(json);
            playerScores = data.playerScores ?? new List<PlayerScore>();
        }
        else
        {
            playerScores = new List<PlayerScore>();
        }
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

[System.Serializable]
public class PlayerScore
{
    public string Name;
    public int Score;
}

[System.Serializable]
 class PlayerScoresData
{
    public List<PlayerScore> playerScores;
}