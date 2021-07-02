using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI highScoreText;

    private void Start()
    {
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        highScoreText.text = $"Best Score : {PersistentManager.Instance.CurrentHighScore.playerName} : {PersistentManager.Instance.CurrentHighScore.highScore}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnEnterName(string name)
    {
        PersistentManager.Instance.CurrentPlayerName = name;
    }
}
