using System.Collections.Generic;
using UnityEngine;

public class BoatVisual : MonoBehaviour
{
    [SerializeField] List<GameObject> trashVisual;

    private void Update()
    {
        int trashValue = Player.Instance.GetCurrentTrashValue();
        int maxTrashValue = Player.Instance.GetMaxTrashValue();

        if (trashValue == maxTrashValue)
        {
            trashVisual[2].gameObject.SetActive(true);
        }
        else if (trashValue >= maxTrashValue / 3 * 2)
        {
            trashVisual[1].gameObject.SetActive(true);
        }
        else if (trashValue >= maxTrashValue / 3)
        {
            trashVisual[0].gameObject.SetActive(true);
        }
        else
        {
            foreach (GameObject trash in trashVisual)
            {
                trash.gameObject.SetActive(false);
            }
        }
    }
}
