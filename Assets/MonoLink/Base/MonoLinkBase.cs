using Leopotam.Ecs;
using UnityEngine;

public abstract class MonoLinkBase : MonoBehaviour
{
    public abstract void Make(ref EcsEntity entity);
}
