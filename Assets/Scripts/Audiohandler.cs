using UnityEngine;
using UnityEngine.Audio;

public class Audiohandler : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource musicSource;

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
}


