using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public Image blackOut;
    private Color color;

    private Volume postProcessingVolume;
    private Bloom bloom;
    private Coroutine fadeCoroutine;

    void Start()
    {
        postProcessingVolume = GetComponent<Volume>();
        postProcessingVolume.profile.TryGet(out bloom);
        PlaySound("Audio/Intro");
        color = blackOut.color;
        FadeOutBloom(2f);
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

    public void FadeOutBloom(float duration)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine); // Stop any existing coroutine to avoid conflicts
        }
        fadeCoroutine = StartCoroutine(FadeBloomToZero(duration));
    }

    IEnumerator FadeBloomToZero(float duration)
    {
        if (bloom == null) yield break;

        yield return new WaitForSeconds(5f);

        float startIntensity = bloom.intensity.value; // Current bloom intensity
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float newIntensity = Mathf.Lerp(startIntensity, 0f, timeElapsed / duration);
            color.a = Mathf.Max(color.a - 0.01f, 0f);
            blackOut.color = color;
            bloom.intensity.value = newIntensity;
            yield return null;
        }

        bloom.intensity.value = 0f;
        blackOut.enabled = false;
        color.a = 1f;
        blackOut.color = color;
    }
}
