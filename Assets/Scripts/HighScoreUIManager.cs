using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class HighScoreUIManager : MonoBehaviour
{
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI highScoreText;
    private GameObject textObject;
    public ScoreArray scoreArray;

    // Start is called before the first frame update
    void Start()
    {
        scoreArray.LoadScore();
        yourScoreText.text = $"Your Score:\n{Transfer.Instance.playerName}: {Transfer.Instance.score}";
        foreach (int index in Enumerable.Range(0, 5))
        {
            //transform.GetChild(index).GetComponent<TextMeshPro>().text = $"{scoreArray.highScoreName[index]}: {scoreArray.highScoreName}";
            textObject = highScoreText.transform.GetChild(index).gameObject;
            Debug.Log(textObject.name);
            textObject.GetComponent<TextMeshProUGUI>().text = $"{scoreArray.highScoreName[index]}: {scoreArray.highScorePoints[index]}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
