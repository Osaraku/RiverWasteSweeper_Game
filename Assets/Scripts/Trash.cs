using System;
using UnityEngine;
using UnityEngine.Events;

public class Trash : MonoBehaviour
{
    public event EventHandler OnTrashCollected;

    [SerializeField] String tagFilter;
    [SerializeField] int trashValue;
    [SerializeField] UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        if (Player.Instance.GetCurrentTrashValue() < Player.Instance.GetMaxTrashValue())
        {
            onTriggerEnter.Invoke();
            Player.Instance.IncreaseTrashValue(trashValue);
            OnTrashCollected?.Invoke(this, EventArgs.Empty);

            Destroy(gameObject);
        }
    }
}
