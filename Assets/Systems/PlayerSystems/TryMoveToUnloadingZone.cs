using Leopotam.Ecs;
using UnityEngine;

public class TryMoveToUnloadingZone : IEcsRunSystem
{
    private EcsFilter<PlayerTag, OnTriggerEnterEvent> _playerOnTrigger = null;
    private SceneData _sceneData = null;

    public void Run()
    {
        foreach (int index in _playerOnTrigger)
        {
            ref var entity = ref _playerOnTrigger.GetEntity(index);
            var triggerEvent = entity.Get<OnTriggerEnterEvent>();
            if (triggerEvent.Collider.CompareTag(_sceneData.Asset.UnloadingZone))
            {
                entity.Get<OnUnloadTag>();
            }
        }
    }
}
