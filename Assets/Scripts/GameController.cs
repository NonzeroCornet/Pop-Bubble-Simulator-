using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public float distance = 0;
    public float speed = 5f;
    public TextMeshProUGUI distanceText;

    // Wetness
    public float wetness = 0;
    public TextMeshProUGUI wetnessText;

    // Top scores
    public int topScores = 0;
    public TextMeshProUGUI topScoresText;

    void Awake()
    {
        Debug.Log("Awaking game!");
        distanceText.text = $"Distance traveled: {distance:F2} units";
        wetnessText.text = $"Wetness: {wetness:F2} units";
        topScoresText.text = $"Top Scores: {topScores}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting game!");
        LoadGameData();
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;
        distanceText.text = $"Distance traveled: {distance:F2} units";

        wetness -= Time.deltaTime;
        wetnessText.text = $"Wetness: {wetness:F2} units";
    }

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            distance = this.distance,
            wetness = this.wetness,
            topScores = this.topScores.ToString()
        };
        SaveSystem.Instance.SaveData(data);
    }

    public void LoadGameData()
    {
        GameData data = SaveSystem.Instance.GetData();
        if (data != null)
        {
            distance = data.distance;
            wetness = data.wetness;
            int.TryParse(data.topScores, out topScores);
        }
    }
}