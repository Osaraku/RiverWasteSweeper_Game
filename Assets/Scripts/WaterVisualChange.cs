using UnityEngine;
using WaterSystem;
using WaterSystem.Data;

public class WaterVisualChange : MonoBehaviour
{
    [SerializeField] Water water;
    public Gradient newAbsorptionRamp;
    public Gradient newScatterRamp;
    public float newVisibility = 50f;
    public float transitionDuration = 3f;

    private void Update()
    {
        if (Player.Instance.GetTotalTrashValue() == 3)
        {
            ApplyChanges();
        }
    }

    public void ApplyChanges()
    {
        if (Water.Instance != null)
        {
            Water.Instance.SmoothUpdateWaterSettings(newVisibility, newAbsorptionRamp, newScatterRamp, transitionDuration);
        }
    }
}
