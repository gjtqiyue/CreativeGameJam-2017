using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreSaveLoad : MonoSingleton<ScoreSaveLoad> {

    public static Dictionary<string, List<int>> scores = new Dictionary<string, List<int>>();
    public static List<int> sortedScores = new List<int>();
    public static List<string> sortedNames = new List<string>();

    public void Awake()
    {
        Load();
        Sort();
        print(Application.persistentDataPath);
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, ScoreSaveLoad.scores);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            ScoreSaveLoad.scores = (Dictionary<string, List<int>>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void AddScore(string name, int score)
    {
        if (!scores.ContainsKey(name))
        {
            scores[name] = new List<int>();
            Debug.Log("New list");
        }
        scores[name].Add(score);
    }

    public static void Sort()
    {
        foreach (KeyValuePair<string, List<int>> entry in scores)
        {
            List<int> listScores = entry.Value;
            foreach( int score in listScores)
            {
                sortedScores.Add(score);
                sortedNames.Add(entry.Key);
            }
        }

        // sort
        int n = sortedScores.Count;
        int k;
        for (int m = n; m >= 0; m--)
        {
            for (int i = 0; i < n - 1; i++)
            {
                k = i + 1;
                if (sortedScores[i] < sortedScores[k])
                {
                    int tempScore = sortedScores[i];
                    string tempName = sortedNames[i];

                    sortedScores[i] = sortedScores[k];
                    sortedNames[i] = sortedNames[k];

                    sortedScores[k] = tempScore;
                    sortedNames[k] = tempName;
                }
            }
        }
        Debug.Log("sorted");
    }


}
