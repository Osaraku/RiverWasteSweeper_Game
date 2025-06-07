using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveableTrash : MonoBehaviour
{
    public static event EventHandler OnTrashSold;

    [SerializeField] String tagFilter;
    [SerializeField] int trashValue;
    [SerializeField] Transform parentBasePosition;
    [SerializeField] Transform visual;
    [SerializeField] CapsuleCollider triggerCollider;
    [SerializeField] UnityEvent onTriggerEnter;

    private void Update()
    {
        triggerCollider.center = visual.position - parentBasePosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        Player.Instance.IncreaseTotalTrashValue(trashValue);
        Player.Instance.IncreaseGoldAmount(trashValue);

        OnTrashSold?.Invoke(this, EventArgs.Empty);
        onTriggerEnter.Invoke();
        // Destroy(gameObject);
    }

}
