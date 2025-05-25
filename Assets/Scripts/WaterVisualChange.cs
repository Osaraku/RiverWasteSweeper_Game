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
    [SerializeField] private Water water;

    [SerializeField] private List<WaterColor> waterColor;

    private int waterColorIndex = 0;

    // private void Awake()
    // {
    //     water = GetComponent<Water>();
    // }

    private void Start()
    {
        ApplyChanges(startVisibility, startAbsorptionRamp, startScatterRamp, startDuration);
    }

    private void Update()
    {
        int playerTotalTrashValue = Player.Instance.GetTotalTrashValue();

        if (playerTotalTrashValue == waterColor[waterColorIndex].trashValue)
        {
            Debug.Log("ChangeWater");
            ApplyChanges(waterColor[waterColorIndex].visibility, waterColor[waterColorIndex].absorptionRamp, waterColor[waterColorIndex].scatterRamp, transitionDuration);

            if (waterColorIndex + 1 < waterColor.Count)
            {
                waterColorIndex++;
            }
        }
    }

    public void ApplyChanges(float newVisibility, Gradient newAbsorptionRamp, Gradient newScatterRamp, float transitionDuration)
    {
        if (water != null)
        {
            water.SmoothUpdateWaterSettings(newVisibility, newAbsorptionRamp, newScatterRamp, transitionDuration);
        }
    }
}

[System.Serializable]
public class WaterColor
{
    public int trashValue;
    public float visibility;
    public Gradient absorptionRamp;
    public Gradient scatterRamp;
}
