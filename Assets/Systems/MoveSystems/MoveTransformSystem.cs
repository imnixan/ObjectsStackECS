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
            Debug.Log("Filter index: " + index);
            ref EcsEntity entity = ref _filter.GetEntity(index);
            Debug.Log("Entity: " + entity);
            ref var transform = ref _filter.Get1(index);
            Debug.Log("Transform: " + transform);
            Debug.Log("Transform value: " + transform.Value);
            var newPosition = entity.Get<Position>();
            Debug.Log("New position: " + newPosition);
            transform.Value.position = newPosition.World;
            Debug.Log("Position set to: " + newPosition.World);
        }
    }
}
