using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float distance = 0;
    public float speed = 5f;
    public TextMeshProUGUI distanceText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Starting game!");
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;
        distanceText.text = $"Distance traveled: {distance:F2} units";
    }
}

