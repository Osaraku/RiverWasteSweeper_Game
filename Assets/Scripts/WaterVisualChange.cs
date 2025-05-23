using System.Collections.Generic;
using UnityEngine;
using WaterSystem;

public class WaterVisualChange : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 5f;
    private float startDuration = 3f;

    [Header("StartingColor")]
    [SerializeField] private float startVisibility = 50f;
    [SerializeField] private Gradient startAbsorptionRamp;
    [SerializeField] private Gradient startScatterRamp;

    [SerializeField] private List<WaterColor> waterColor;

    private void Start()
    {
        ApplyChanges(startVisibility, startAbsorptionRamp, startScatterRamp, startDuration);
    }

    private void Update()
    {
        if (Player.Instance.GetTotalTrashValue() == 3)
        {
            ApplyChanges(waterColor[0].Visibility, waterColor[0].AbsorptionRamp, waterColor[0].ScatterRamp, transitionDuration);
        }
    }

    public void ApplyChanges(float newVisibility, Gradient newAbsorptionRamp, Gradient newScatterRamp, float transitionDuration)
    {
        if (Water.Instance != null)
        {
            Water.Instance.SmoothUpdateWaterSettings(newVisibility, newAbsorptionRamp, newScatterRamp, transitionDuration);
        }
    }
}

[System.Serializable]
public class WaterColor
{
    public int trashValue;
    public float Visibility;
    public Gradient AbsorptionRamp;
    public Gradient ScatterRamp;
}
