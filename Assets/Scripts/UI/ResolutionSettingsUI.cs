using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettingsUI : MonoBehaviour
{
    private const string PLAYER_PREFS_RESOLUTION = "Resolution";
    private const string PLAYER_PREFS_IS_FULLSCREEN = "IsFullscreen";

    [SerializeField] private GameObject visual;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] allResolution;
    private bool isFullscreen;
    private int selectedResolution;
    List<Resolution> selectedResolutionList = new List<Resolution>();

    private void Start()
    {
        allResolution = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string resolutionText;

        for (int i = 0; i < allResolution.Length; i++)
        {
            Resolution resolution = allResolution[i];
            resolutionText = resolution.width + " x " + resolution.height;
            if (!resolutionStringList.Contains(resolutionText))
            {
                resolutionStringList.Add(resolutionText);
                selectedResolutionList.Add(resolution);
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionStringList);

        // Load saved settings
        selectedResolution = PlayerPrefs.GetInt(PLAYER_PREFS_RESOLUTION, selectedResolutionList.Count - 1); // Default to last
        isFullscreen = PlayerPrefs.GetInt(PLAYER_PREFS_IS_FULLSCREEN, 1) == 1;

        // Clamp if resolution index is out of range
        selectedResolution = Mathf.Clamp(selectedResolution, 0, selectedResolutionList.Count - 1);

        // Apply loaded settings
        resolutionDropdown.value = selectedResolution;
        fullscreenToggle.isOn = isFullscreen;
        Screen.SetResolution(
            selectedResolutionList[selectedResolution].width,
            selectedResolutionList[selectedResolution].height,
            isFullscreen
        );

        // Add listeners after setting values to avoid triggering callbacks on init
        resolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(); });
        fullscreenToggle.onValueChanged.AddListener(delegate { ChangeFullscreen(); });
    }

    public void ChangeResolution()
    {
        selectedResolution = resolutionDropdown.value;
        Screen.SetResolution(
            selectedResolutionList[selectedResolution].width,
            selectedResolutionList[selectedResolution].height,
            isFullscreen
        );

        PlayerPrefs.SetInt(PLAYER_PREFS_RESOLUTION, selectedResolution);
        PlayerPrefs.Save();
    }

    public void ChangeFullscreen()
    {
        isFullscreen = fullscreenToggle.isOn;
        Screen.SetResolution(
            selectedResolutionList[selectedResolution].width,
            selectedResolutionList[selectedResolution].height,
            isFullscreen
        );

        PlayerPrefs.SetInt(PLAYER_PREFS_IS_FULLSCREEN, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
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
