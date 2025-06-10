using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private GameObject optionVisual;
    [SerializeField] private Button closeButton;
    [SerializeField] private VolumeSettingsUI volumeSettingsUI;
    [SerializeField] private ResolutionSettingsUI resolutionSettingsUI;

    private Action onClosedButtonAction;

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(CloseCLick);
    }

    private void CloseCLick()
    {
        Hide();
        volumeSettingsUI.Hide();
        resolutionSettingsUI.Hide();
        onClosedButtonAction();
    }

    private void Start()
    {
        Hide();
    }

    public void Show(Action onClosedButtonAction)
    {
        this.onClosedButtonAction = onClosedButtonAction;

        optionVisual.gameObject.SetActive(true);
        volumeSettingsUI.Show();
        resolutionSettingsUI.Show();
    }

    public void Hide()
    {
        optionVisual.gameObject.SetActive(false);
        volumeSettingsUI.Hide();
        resolutionSettingsUI.Hide();
    }

}
