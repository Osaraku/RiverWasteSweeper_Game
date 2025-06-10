using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettingsUI : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";

    [SerializeField] private GameObject visual;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Load saved volume settings, or use default (1.0f) if not found
        float savedMusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);

        // Set sliders to saved values
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;

        SetMusicVolume();

        // Add listeners to sliders to handle changes
        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });
    }

    private void OnSFXVolumeChanged()
    {
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, sfxVolume);
        PlayerPrefs.Save();
    }

    private void OnMusicVolumeChanged()
    {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        float sfxVolume = sfxSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void Show()
    {
        visual.SetActive(true);
    }

    public void Hide()
    {
        visual.SetActive(false);
    }
}
