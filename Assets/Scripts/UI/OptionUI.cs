using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private GameObject optionVisual;
    [SerializeField] private Button closeButton;
    [SerializeField] private VolumeSettingsUI volumeSettingsUI;

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(CloseCLick);
    }

    private void CloseCLick()
    {
        Hide();
        volumeSettingsUI.Hide();
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        optionVisual.gameObject.SetActive(true);
        volumeSettingsUI.Show();
    }

    public void Hide()
    {
        optionVisual.gameObject.SetActive(false);
    }

}
