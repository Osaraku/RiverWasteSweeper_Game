using System;
using UnityEngine;
using UnityEngine.Events;

public class BuyableBoat : MonoBehaviour
{
    [SerializeField] String tagFilter;
    [SerializeField] int price;
    [SerializeField] int boatLevel;
    [SerializeField] int speedIncrease;
    [SerializeField] int trashStorageIncrease;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }

    public void Interact()
    {
        int playerGold = Player.Instance.GetGoldAmount();

        if (playerGold >= price)
        {
            playerGold -= price;

            Player.Instance.SetGoldAmount(playerGold);
            Player.Instance.BoatUpgrade(boatLevel, speedIncrease, trashStorageIncrease);

            Destroy(gameObject);
        }
    }

    public int GetPrice()
    {
        return price;
    }
}
