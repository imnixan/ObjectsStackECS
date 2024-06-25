using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Assets", fileName = "Asset", order = 0)]
public class Assets : ScriptableObject
{
    public AnimationClip moveAnim;
    public AnimationClip idleAnim;

    public string ItemTag = "Item";
    public string UnloadingZone = "UnloadingZone";
    public float SpawnTimer;

    public GameObject PancakePrefab;
    public GameObject PancakeStackPrefab;

    public Range PancakesSpawnRange;
}

[Serializable]
public struct Range
{
    public float min;
    public float max;
}
