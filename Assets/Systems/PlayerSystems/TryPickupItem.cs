using Leopotam.Ecs;
using UnityEngine;

public class TryPickupItem : IEcsRunSystem
{
    private EcsFilter<PlayerTag, OnCollisionEnterEvent> _playerCollision = null;
    private EcsFilter<OnCollisionEnterEvent> _onCollisionEnter = null;
    private SceneData _sceneData = null;

    public void Run()
    {
        foreach (int index in _playerCollision)
        {
            ref var entity = ref _playerCollision.GetEntity(index);
            ref var rb = ref entity.Get<RigidbodyLink>();

            var collisionEvent = entity.Get<OnCollisionEnterEvent>();
            var collObject = collisionEvent.Collision.gameObject;
            if (collObject.CompareTag(_sceneData.asset.ItemTag))
            {
                foreach (int collIndex in _onCollisionEnter)
                {
                    ref var collEntity = ref _onCollisionEnter.GetEntity(collIndex);
                    if (collEntity.Get<GameObjectLink>().Value == collObject)
                    {
                        Debug.Log("Add d tag");
                        collEntity.Get<DestroyTag>();
                    }
                }
            }
        }
    }
}
