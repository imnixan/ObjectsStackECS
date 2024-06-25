using System;
using UnityEngine;

public class TransformMonoLink : MonoLink<TransformLink>
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Value.Value == null)
        {
            Value = new TransformLink { Value = GetComponent<Transform>() };
        }
    }
#endif
}
