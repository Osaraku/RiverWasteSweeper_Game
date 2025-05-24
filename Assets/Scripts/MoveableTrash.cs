using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveableTrash : MonoBehaviour
{
    [SerializeField] String tagFilter;
    [SerializeField] int trashValue;
    [SerializeField] Transform visual;
    [SerializeField] CapsuleCollider capsuleCollider;

    private void Update()
    {
        capsuleCollider.center = visual.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        Player.Instance.IncreaseTotalTrashValue(trashValue);
        Player.Instance.IncreaseGoldAmount(trashValue);
        Destroy(gameObject);
    }

}
