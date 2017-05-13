using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreSaveLoad : Singleton<ScoreSaveLoad> {

    public static List<int> scores = new List<int>();

    public static void Save(List<int> scoresList)
    {
        ScoreSaveLoad.scores = scoresList;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedScores.gd");
        bf.Serialize(file, ScoreSaveLoad.scores);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            ScoreSaveLoad.scores = (List<int>)bf.Deserialize(file);
            file.Close();
        }
    }
}
