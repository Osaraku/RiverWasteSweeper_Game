using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrashValueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trashValueText;
    [SerializeField] private Image barImage;

    private void Update()
    {
        float trashValue = Player.Instance.GetCurrentTrashValue();
        float maxTrashValue = Player.Instance.GetMaxTrashValue();

        trashValueText.text = trashValue.ToString() + "/" + maxTrashValue.ToString();

        barImage.fillAmount = trashValue / maxTrashValue;
    }
}
