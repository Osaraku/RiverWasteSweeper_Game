using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private GameObject optionVisual;
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
        Hide();
        OptionUI.Instance.Show(Show);
    }

    private void Start()
    {
        Hide();

        Player.Instance.OnGamePaused += Player_OnGamePaused;
        Player.Instance.OnGameUnpaused += Player_OnGameUnpaused;
    }

    private void Player_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
        OptionUI.Instance.Hide();
    }

    private void Player_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        pauseVisual.SetActive(true);
    }

    private void Hide()
    {
        pauseVisual.SetActive(false);
    }
}
