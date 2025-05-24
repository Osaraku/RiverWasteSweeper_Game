using TMPro;
using UnityEngine;

public class GoldAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldAmountText;

    private void Update()
    {
        goldAmountText.text = Player.Instance.GetGoldAmount().ToString();
    }
}
