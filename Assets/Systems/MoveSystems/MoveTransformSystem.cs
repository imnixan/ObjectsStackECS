using Leopotam.Ecs;
using UnityEngine;

public class MoveTransformSystem : IEcsRunSystem
{
    private EcsFilter<TransformLink, MoveTag> _filter = null;

    public void Run()
    {
        if (_filter.IsEmpty())
        {
            return;
        }

        foreach (int index in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(index);
            ref var transform = ref _filter.Get1(index);
            var newPosition = entity.Get<Position>();

            transform.Value.position = newPosition.World;
        }
    }
}
