using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] int trashValueRequired;


    private void Update()
    {
        if (Player.Instance.GetTotalTrashValue() >= trashValueRequired)
        {
            Destroy(gameObject);
        }
    }
}
