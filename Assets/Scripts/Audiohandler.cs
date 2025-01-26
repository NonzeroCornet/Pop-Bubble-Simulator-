using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audiohandler : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource narratorSource;
    public List<Narrator> narrators = new List<Narrator>();
    private List<AudioClip> narrationClips = new List<AudioClip>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        SetVolume();
    }

    public void SetMusic(AudioClip music)
    {
        if (music == null)
        {
            music = musicSource.clip;
        }
        musicSource.clip = music;
        musicSource.Play();
    }

    public void AddNarrationClips(Narrator narrator)
    {
        if (narrator == null || narrator.clips == null || narrator.clips.Length == 0)
        {
            return;
        }

        int index = narrators.IndexOf(narrator);
        if (index == -1)
        {
            narrators.Add(narrator);
        }
        else
        {
            narrators[index] = narrator;
        }

        narrationClips.AddRange(narrator.clips);
    }

    public void PlayNextNarration()
    {
        if (narrationClips.Count == 0)
        {
            return;
        }

        AudioClip clip = narrationClips[0];
        narrationClips.RemoveAt(0);
        narratorSource.clip = clip;
        narratorSource.Play();
    }
}

