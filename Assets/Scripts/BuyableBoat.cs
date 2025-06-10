using System;
using UnityEngine;
using UnityEngine.Events;

public class BuyableBoat : MonoBehaviour
{
    public static event EventHandler OnBoatPurchased;

    public static void ResetStaticData()
    {
        OnBoatPurchased = null;
    }

    [SerializeField] String tagFilter;
    [SerializeField] int price;
    [SerializeField] int boatLevel;
    [SerializeField] int speedIncrease;
    [SerializeField] int rotationIncrease;
    [SerializeField] int trashStorageIncrease;
    [SerializeField] GameObject interactArea;
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
            Player.Instance.BoatUpgrade(boatLevel, speedIncrease, rotationIncrease, trashStorageIncrease);
            OnBoatPurchased?.Invoke(this, EventArgs.Empty);

            Destroy(interactArea);
            Destroy(gameObject);
        }
    }

    public int GetPrice()
    {
        return price;
    }
}
