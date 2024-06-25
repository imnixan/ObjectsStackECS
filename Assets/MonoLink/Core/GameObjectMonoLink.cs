using System.Runtime.CompilerServices;
using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(MonoEntity))]
public class GameObjectMonoLink : MonoLink<GameObjectLink>
{
    public override void Make(ref EcsEntity entity)
    {
        entity.Get<GameObjectLink>() = new GameObjectLink { Value = gameObject };
    }
}
