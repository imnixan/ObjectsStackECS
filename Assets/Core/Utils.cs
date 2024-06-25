using UnityEngine;

public static class Utils
{
    public static float GetRandomInMinMaxRange(float min, float max)
    {
        int rangeSelector = Random.Range(0, 2);
        if (rangeSelector == 0)
        {
            return Random.Range(-min, -max);
        }
        else
        {
            return Random.Range(min, max);
        }
    }
}
