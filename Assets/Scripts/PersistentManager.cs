using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }

    public string CurrentPlayerName { get; set; }

    public SaveData CurrentHighScore { get; private set; }

    public void RecordNewScore(int score)
    {
        if (score > CurrentHighScore.highScore)
        {
            string path = Application.persistentDataPath + "/savedata.json";

            string json = JsonUtility.ToJson(new SaveData(CurrentPlayerName, score));

            File.WriteAllText(path, json);

            LoadHighScore();
        }
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData savedData = JsonUtility.FromJson<SaveData>(json);

            CurrentHighScore = savedData;
        }
    }

    [System.Serializable]
    public struct SaveData
    {
        public string playerName;

        public int highScore;

        public SaveData(string playerName, int highScore)
        {
            this.playerName = playerName;
            this.highScore = highScore;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }
}
