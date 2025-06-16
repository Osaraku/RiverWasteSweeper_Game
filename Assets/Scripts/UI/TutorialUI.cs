using System;
using UnityEngine;
using System.Collections;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject controlTutorial;
    [SerializeField] GameObject trashTutorial;
    [SerializeField] GameObject largeTrashTutorial;
    [SerializeField] GameObject storageFullTutorial;

    private bool hasControlTutorialShown = false;
    private bool hasTrashTutorialShown = false;
    private bool hasLargeTrashTutorialShown = false;
    private bool hasStorageFullTutorialShown = false;

    private float delayTime = 10f;

    private void Start()
    {
        Show(controlTutorial);
        Hide(trashTutorial);
        Hide(largeTrashTutorial);
        Hide(storageFullTutorial);

        Trash.OnTrashCollected += Trash_OnTrashCollected;
        Trash.OnTrashNotCollected += Trash_OnTrashNotCollected;
        MoveableTrashTutorialTrigger.OnTutorialAreaTriggered += MoveableTrashTutorialTrigger_OnTutorialAreaTriggered;
        MoveableTrash.OnTrashSold += MoveableTrash_OnTrashSold;
    }

    private void MoveableTrash_OnTrashSold(object sender, EventArgs e)
    {
        Hide(largeTrashTutorial);
        hasLargeTrashTutorialShown = true;
    }

    private void MoveableTrashTutorialTrigger_OnTutorialAreaTriggered(object sender, EventArgs e)
    {
        Show(largeTrashTutorial);
    }

    private void Trash_OnTrashNotCollected(object sender, EventArgs e)
    {
        if (hasTrashTutorialShown)
        {
            float storageFullDelayTime = 5f;

            Show(storageFullTutorial);
            StartCoroutine(HideAfterDelay(storageFullTutorial, storageFullDelayTime, () => hasStorageFullTutorialShown = true));
        }
    }

    private void Trash_OnTrashCollected(object sender, EventArgs e)
    {
        if (!hasTrashTutorialShown && hasControlTutorialShown)
        {
            Show(trashTutorial);
            StartCoroutine(HideAfterDelay(trashTutorial, delayTime, () => hasTrashTutorialShown = true));
        }
    }

    private void Update()
    {
        if (Player.Instance.GetIsMoving() == true && !hasLargeTrashTutorialShown)
        {
            StartCoroutine(HideAfterDelay(controlTutorial, delayTime, () => hasControlTutorialShown = true));
        }
    }

    private void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private IEnumerator HideAfterDelay(GameObject gameObject, float delay, Action setShown)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        setShown?.Invoke();
    }

    private void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
