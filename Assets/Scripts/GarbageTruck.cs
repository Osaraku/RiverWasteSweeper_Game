using System;
using UnityEngine;
using UnityEngine.Events;

public class GarbageTruck : MonoBehaviour
{
    public static event EventHandler OnTrashSold;

    [SerializeField] String tagFilter;
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
        int goldEarned = Player.Instance.GetCurrentTrashValue();
        Player.Instance.SetCurrentTrashValue(0);
        Player.Instance.IncreaseGoldAmount(goldEarned);
        OnTrashSold?.Invoke(this, EventArgs.Empty);
    }
}
