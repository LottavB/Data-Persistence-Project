using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text bestScoreText;
    
    private bool m_Started = false;
    public int m_Points;

    public ScoreArray scoreArray;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        scoreArray.LoadScore();
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        AddPoint(0);
        bestScoreText.text = $"Best Score: {scoreArray.highScoreName[0]}: {scoreArray.highScorePoints[0]}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {Transfer.Instance.playerName}: {m_Points}";
    }

    public void GameOver()
    {
        Transfer.Instance.score = m_Points;
        GameOverText.SetActive(true);
        StartCoroutine(GameOverCountdown());

        // Add score to the high score array if nescesary
        if (scoreArray.highScorePoints[4] == 0)
        {
            foreach (int index in Enumerable.Range(0, 5))
            {
                if (scoreArray.highScorePoints[index] == 0)
                {
                    Debug.Log("append op index:" + index);
                    scoreArray.highScorePoints[index] = m_Points;
                    scoreArray.highScoreName[index] = Transfer.Instance.playerName;
                    Array.Sort(scoreArray.highScorePoints, scoreArray.highScoreName);
                    Array.Reverse(scoreArray.highScoreName);
                    Array.Reverse(scoreArray.highScorePoints);
                    scoreArray.SaveScore();
                    break;
                }
            }
        }
        else
        {
            if (m_Points > scoreArray.highScorePoints[4])
            {
                Debug.Log("Append aan eind");
                scoreArray.highScorePoints[4] = m_Points;
                scoreArray.highScoreName[4] = Transfer.Instance.playerName;
                Array.Sort(scoreArray.highScorePoints, scoreArray.highScoreName);
                Array.Reverse(scoreArray.highScoreName);
                Array.Reverse(scoreArray.highScorePoints);
                scoreArray.SaveScore();
            }
        }

    }

    private IEnumerator GameOverCountdown()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

}

