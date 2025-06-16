using System;
using UnityEngine;
using UnityEngine.Events;

public class MoveableTrashTutorialTrigger : MonoBehaviour
{
    public static event EventHandler OnTutorialAreaTriggered;

    public static void ResetStaticData()
    {
        OnTutorialAreaTriggered = null;
    }

    [SerializeField] String tagFilter;

    private void OnTriggerEnter(Collider other)
    {
        if (!String.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) return;

        OnTutorialAreaTriggered?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
