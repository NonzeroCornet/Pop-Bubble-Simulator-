using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public float distance = 0;
    public float speed = 0.2f;
    public TextMeshProUGUI distanceText;

    // Wetness
    public float wetness = 0;
    public TextMeshProUGUI wetnessText;

    // Top scores
    public int topScores = 0;
    public TextMeshProUGUI topScoresText;

    // No way chat is this a bubble reference????
    public GameObject bubble;

    void Awake()
    {
        Debug.Log("Awaking game!");
        distanceText.text = $"Distance traveled: {distance:F2} units";
        // wetnessText.text = $"Wetness: {wetness:F2} units";
        // topScoresText.text = $"Top Scores: {topScores}";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting game!");
        LoadGameData();
        SaveGameData();
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;
        distanceText.text = $"Distance traveled: {distance:F2} units";

        wetness -= Time.deltaTime;
        // wetnessText.text = $"Wetness: {wetness:F2} units";
        SetBubbleSize();
    }

    public void EndGame()
    {
        SaveGameData();
        Application.Quit();
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

    private void SetBubbleSize()
    {
        float size = Mathf.Clamp(wetness, 1, 100);
        bubble.transform.localScale = new Vector3(size, size, size);
    }
}

