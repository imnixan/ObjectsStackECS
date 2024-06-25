using Leopotam.Ecs;
using UnityEngine;

public class AnimationMonoLink : MonoLink<AnimationLink>
{
    public override void Make(ref EcsEntity entity)
    {
        entity.Get<AnimationLink>() = new AnimationLink
        {
            Value = GetComponentInChildren<Animation>(),
            NextAnimation = "",
            CurrentAnimation = ""
        };
    }
}
