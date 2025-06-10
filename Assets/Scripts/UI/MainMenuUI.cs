using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject gameLogo;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayClick);
        optionButton.onClick.AddListener(OptionClick);
        quitButton.onClick.AddListener(QuitClick);

        Time.timeScale = 1f;
    }

    private void PlayClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OptionClick()
    {
        OptionUI.Instance.Show(Show);
        Hide();
    }

    private void QuitClick()
    {
        Application.Quit();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        gameLogo.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        gameLogo.SetActive(false);
    }


}
