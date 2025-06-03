using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveableTrash : MonoBehaviour
{
    [SerializeField] String tagFilter;
    [SerializeField] int trashValue;
    [SerializeField] Transform parentBasePosition;
    [SerializeField] Transform visual;
    [SerializeField] CapsuleCollider triggerCollider;

    private void Update()
    {
        triggerCollider.center = visual.position - parentBasePosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        Player.Instance.IncreaseTotalTrashValue(trashValue);
        Player.Instance.IncreaseGoldAmount(trashValue);
        Destroy(gameObject);
    }

}
