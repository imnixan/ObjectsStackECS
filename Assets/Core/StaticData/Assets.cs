using UnityEngine;

[CreateAssetMenu(menuName = "Config/Assets", fileName = "Asset", order = 0)]
public class Assets : ScriptableObject
{
    public AnimationClip moveAnim;
    public AnimationClip idleAnim;

    public string ItemTag = "Item";
    public string UnloadingZone = "UnloadingZone";
}
