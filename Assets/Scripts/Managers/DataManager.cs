using UnityEngine;

public class DataManager : PersistentSingleton<DataManager>
{
    public float GlobalHeatTotal;

    public float valueHeat = 1f;
    public float bonusFallSpeed = 1f;

    [Min(0.01f)]
    public float timeBetweenSpawns = 5f;

    [Min(0f)]
    public float maxSpawnDuration = 20f;

    public float SpeedBarrel = 1f;
    public float ScaleBarrel = 1f;

    public void UpdateGlobalHeat(float value)
    {
        GlobalHeatTotal += value;
    }

    public void ResetData()
    {
        GlobalHeatTotal = 1;
        valueHeat = 1f;
        bonusFallSpeed = 1f;
        timeBetweenSpawns = 5f;
        maxSpawnDuration = 20f;
        SpeedBarrel = 1f;
        ScaleBarrel = 1f;

    }
}
