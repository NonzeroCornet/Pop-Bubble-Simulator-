using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleController : MonoBehaviour
{

    public GameObject ded;
    public GameObject ded2;
    public GameObject ded3;
    public GameObject worldObject;
    public GameObject cameraObject;
    public GameObject audioHandler;
    public GameObject audioPlayer;
    public GameObject canvasObject;

    private bool isDed = false;

    private bool popAnim = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDed)
        {
            ded.transform.localScale = Vector3.Lerp(ded.transform.localScale, new Vector3(0.4f, 0.4f, 0.4f), 5*Time.deltaTime);
            if (new Vector3(Mathf.Round(ded.transform.localScale.x*10f), Mathf.Round(ded.transform.localScale.y * 10f), Mathf.Round(ded.transform.localScale.z * 10f)) == new Vector3(4f, 4f, 4f))
            {
                ded2.SetActive(true);
                if (popAnim == false)
                {
                    popAnim = true;
                    StartCoroutine(PopAnimation());
                }
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    public void PlaySound(string soundDirectory)
    {
        // Load the audio clip from the Resources folder
        AudioClip clip = Resources.Load<AudioClip>(soundDirectory);

        if (clip != null)
        {
            // Create an AudioSource if one doesn't exist on this GameObject
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Play the loaded audio clip
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"Sound at directory '{soundDirectory}' not found. Ensure the file is in the Resources folder and the path is correct.");
        }
    }

    IEnumerator PopAnimation()
    {
        PlaySound("Audio/pop");

        ded3.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        ded3.SetActive(false);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game");
    }

    public void DeadBubble()
    {
        worldObject.GetComponent<MapController>().enabled = false;
        cameraObject.GetComponent<CameraController>().enabled = false;
        audioHandler.SetActive(false);
        audioPlayer.SetActive(false);
        canvasObject.SetActive(false);
        PlaySound("Audio/DeathNormal");

        isDed = true;
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

        DeadBubble();
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

