using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBMonoLink : MonoLink<RigidbodyLink>
{
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Value.Value == null)
        {
            Value = new RigidbodyLink { Value = GetComponent<Rigidbody>() };
        }
    }
#endif
}
