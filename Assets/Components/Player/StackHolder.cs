using System;
using UnityEngine;

[Serializable]
public struct StackHolder
{
    public int MaxStackSize;
    public int CurrentStack;
    public float UnloadDelay;
    public float LastUnloadTime;
}
