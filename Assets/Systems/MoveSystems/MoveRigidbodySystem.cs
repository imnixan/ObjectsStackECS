using Leopotam.Ecs;
using UnityEngine;

public class MoveRigidbodySystem : IEcsRunSystem
{
    private EcsFilter<RigidbodyLink, MoveTag> _filter = null;

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
            var newPosition = entity.Get<Position>();

            rigidbody.Value.MovePosition(newPosition.World);
        }
    }
}
