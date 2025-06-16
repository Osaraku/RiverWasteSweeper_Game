using System;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private List<FishData> fishDataList;


    private void Update()
    {
        int totalPlayerTrashValue = Player.Instance.GetTotalTrashValue();

        foreach (var fishData in fishDataList)
        {
            if (totalPlayerTrashValue >= fishData.trashValueRequired && !fishData.fish.activeSelf)
            {
                Show(fishData);
            }
        }
    }

    private void Show(FishData fishData)
    {
        fishData.fish.SetActive(true);
    }
}

[System.Serializable]
public class FishData
{
    public GameObject fish;
    public int trashValueRequired;
}