using System;
using UnityEngine;

[Serializable]
public struct EcsJoystick
{
    public RectTransform Background;
    public RectTransform Knob;

    public float Horizontal;
    public float Vertical;

    public float offset;
    Vector2 PointPosition;
}
