using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public TextMeshProUGUI bestScoreText;
    public ScoreArray scoreArray;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    void Start()
    {
        scoreArray.LoadScore();
        bestScoreText.text = $"Best Score: {scoreArray.highScoreName[0]}: {scoreArray.highScorePoints[0]}";
    }

    public void SavePlayerName()
    {
        Transfer.Instance.playerName = inputPlayerName.text;
    }

    public void StartGame()
    {
        if (Transfer.Instance.playerName != null)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
