using System;
using UnityEngine;
using UnityEngine.Events;

public class Trash : MonoBehaviour
{
    [SerializeField] String tagFilter;
    [SerializeField] int trashValue;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        if (Player.Instance.GetCurrentTrashValue() < Player.Instance.GetMaxTrashValue())
        {
            onTriggerEnter.Invoke();
            Player.Instance.IncreaseTrashValue(trashValue);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }
}
