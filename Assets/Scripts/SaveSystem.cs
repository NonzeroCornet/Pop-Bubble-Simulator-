using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("SaveSystem instance created.");
        }
        else if (Instance != this)
        {
            Debug.Log("Duplicate SaveSystem instance found, destroying duplicate.");
            Destroy(gameObject);
        }
    }

    public void SaveData(GameData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.dat";
        FileStream file = File.Create(path);

        bf.Serialize(file, data);
        file.Close();
        Debug.Log($"Game data saved to {path}.");
    }

    public GameData GetData()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            Debug.Log($"Loading game data from {path}.");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game data successfully loaded.");
            return data;
        }

        Debug.LogWarning("No save file found.");
        return null;
    }
}

[System.Serializable]
public class GameData
{
    public float distance;
    public float wetness;
    public string topScores;
}

