using UnityEngine;

public static class GameUtils
{
    public static float GetRandomValue(float minValue, float maxValue)
    {
        var randomValue = Random.Range(minValue, maxValue);
        return randomValue;
    }
}
