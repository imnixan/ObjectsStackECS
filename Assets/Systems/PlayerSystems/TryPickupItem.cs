using Leopotam.Ecs;
using UnityEngine;

public class TryPickupItem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, StackHolder, OnCollisionEnterEvent> _playerCollision = null;
    private EcsFilter<OnCollisionEnterEvent> _onCollisionEnter = null;
    private SceneData _sceneData = null;

    public void Run()
    {
        foreach (int index in _playerCollision)
        {
            ref var entity = ref _playerCollision.GetEntity(index);
            ref var rb = ref entity.Get<RigidbodyLink>();
            ref var stackHolder = ref entity.Get<StackHolder>();

            var collisionEvent = entity.Get<OnCollisionEnterEvent>();
            var collObject = collisionEvent.Collision.gameObject;
            if (collObject.CompareTag(_sceneData.Asset.ItemTag))
            {
                foreach (int collIndex in _onCollisionEnter)
                {
                    ref var collEntity = ref _onCollisionEnter.GetEntity(collIndex);
                    if (collEntity.Get<GameObjectLink>().Value == collObject)
                    {
                        if (stackHolder.CurrentStack + 1 <= stackHolder.MaxStackSize)
                        {
                            collEntity.Get<DestroyTag>();
                            stackHolder.CurrentStack++;
                            ref var pickupTag = ref entity.Get<StackAddEvent>();
                            pickupTag.count++;
                        }
                    }
                }
            }
        }
    }
}
