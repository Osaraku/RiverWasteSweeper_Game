using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseTrash : MonoBehaviour
{
    [SerializeField] String tagFilter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;
        onTriggerEnter.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }
}
