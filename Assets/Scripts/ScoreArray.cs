using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreArray : MonoBehaviour
{
    public int[] highScorePoints = new int[5];
    public string[] highScoreName = new string[5];

    [System.Serializable]
    class SaveData
    {
        public int[] highScorePointsSave = new int[5];
        public string[] highScoreNameSave = new string[5];
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScoreNameSave = highScoreName;
        data.highScorePointsSave = highScorePoints;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log("loading score");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.highScoreNameSave;
            highScorePoints = data.highScorePointsSave;
        }
    }
}
