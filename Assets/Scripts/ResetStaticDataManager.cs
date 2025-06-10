using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        Trash.ResetStaticData();
        Barrier.ResetStaticData();
        GarbageTruck.ResetStaticData();
        MoveableTrash.ResetStaticData();
    }
}
