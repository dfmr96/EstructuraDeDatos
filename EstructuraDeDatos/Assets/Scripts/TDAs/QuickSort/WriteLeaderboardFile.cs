using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WriteLeaderboardFile : MonoBehaviour
{
    private string fileName = "leaderboard.json"; // Cambiado a .json para claridad
    private string filePath;
    public ScoreList scoreList = new ScoreList();

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
            Debug.Log("Archivo creado: " + filePath);
        }
        else
        {
            Debug.Log("Archivo existente: " + filePath);
            string json = File.ReadAllText(filePath);
            ScoreList loadedScores = JsonUtility.FromJson<ScoreList>(json);
            if (loadedScores != null)
            {
                scoreList = loadedScores;
            }
        }
    }

    private void Start()
    {
        SaveScores();
    }

    public void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Puntajes guardados: " + json);
    }

    public List<Score> GetScoreList()
    {
        return scoreList.scores;
    }

    public void SetNewScore(Score newScore)
    {
        scoreList.scores.Add(newScore);
        SaveScores();
    }
}
[System.Serializable]
public class Score
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<Score> scores = new List<Score>();
}