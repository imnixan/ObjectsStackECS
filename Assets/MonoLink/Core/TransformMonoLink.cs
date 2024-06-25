using System;
using Leopotam.Ecs;
using UnityEngine;

public class TransformMonoLink : MonoLink<TransformLink>
{
    public override void Make(ref EcsEntity entity)
    {
        var transformLink = new TransformLink { Value = transform };
        entity.Get<TransformLink>() = transformLink;
    }
}
