using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    //void Start()
    //{
    //    inputPlayerName.placeholder.GetComponent<Text>().text = "Enter name";
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPlayerName()
    {
        Transfer.Instance.playerName = inputPlayerName.text;
        bestScoreText.text = "Best Score: " + Transfer.Instance.playerName;
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
