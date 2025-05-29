using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactButtonText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] BuyableBoat buyableBoat;

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        interactButtonText.text = Player.Instance.GetInteractAction().bindings[0].ToDisplayString();

        if (priceText != null && buyableBoat != null)
        {
            priceText.text = buyableBoat.GetPrice().ToString();
        }
    }
}
