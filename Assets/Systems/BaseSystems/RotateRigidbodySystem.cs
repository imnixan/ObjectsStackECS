using Leopotam.Ecs;
using UnityEngine;

public class RotateRigidbodySystem : IEcsRunSystem
{
    private EcsFilter<RigidbodyLink, Rotation> _filter = null;

    public void Run()
    {
        if (_filter.IsEmpty())
        {
            return;
        }

        foreach (int index in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(index);
            ref var rigidbody = ref entity.Get<RigidbodyLink>();
            var newRotation = entity.Get<Rotation>();

            rigidbody.Value.MoveRotation(newRotation.World);
        }
    }
}
