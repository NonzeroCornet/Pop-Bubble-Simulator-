using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float distance = 0;
    public float speed = 0.2f;
    public TextMeshProUGUI distanceText;

    // Wetness
    public float wetness = 0;
    public TextMeshProUGUI wetnessText;
    public GameObject wetnessPanel;

    // Top scores
    public int topScores = 0;
    public TextMeshProUGUI topScoresText;

    // No way chat is this a bubble reference????
    public GameObject bubble;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        SetWetnessPanelSize();
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

    private void SetWetnessPanelSize()
    {
        float size = Mathf.Clamp(wetness, 0, 100);
        wetnessPanel.GetComponent<RectTransform>().right = new Vector2(size, wetnessPanel.GetComponent<RectTransform>().right.y);
    }

    public void IncrementWetness(float amount)
    {
        wetness += amount;
        SetWetnessPanelSize();
    }
}

