using Leopotam.Ecs;
using UnityEngine;

public class RotateTransformSystem : IEcsRunSystem
{
    private EcsFilter<TransformLink, Rotation> _filter = null;

    public void Run()
    {
        if (_filter.IsEmpty())
        {
            return;
        }

        foreach (int index in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(index);
            ref var transform = ref entity.Get<TransformLink>();
            var newRotation = entity.Get<Rotation>();

            transform.Value.rotation = newRotation.World;
        }
    }
}
