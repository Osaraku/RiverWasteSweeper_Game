using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private GameObject gameLogo;
    [SerializeField] private GameObject pauseVisual;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeClick);
        mainMenuButton.onClick.AddListener(MainMenuClick);
        settingsButton.onClick.AddListener(SettingsClick);
    }

    private void ResumeClick()
    {
        Player.Instance.TogglePauseGame();
    }
    private void MainMenuClick()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
    private void SettingsClick()
    {
        OptionUI.Instance.Show(Show);
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (Player.Instance.GetIsGamePaused() == true)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        pauseVisual.gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide()
    {
        pauseVisual.gameObject.SetActive(false);
        // OptionUI.Instance.Hide();
    }
}
