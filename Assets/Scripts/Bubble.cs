using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DeadBubble()
    {   
        // Animations here later

        // Destroy the bubble
        DestroyBubble();
    }

    public void DestroyBubble()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            return;
        }

        DestroyBubble();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            return;
        }

        // Wetness zones
        if (collision.gameObject.CompareTag("Wetness"))
        {
            GameController.instance.IncrementWetness(10f);
        }
    }
}

