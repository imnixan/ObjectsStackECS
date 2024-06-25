using Leopotam.Ecs;
using UnityEngine;

public class TryLeaveUnloadinZone : IEcsRunSystem
{
    private EcsFilter<PlayerTag, OnTriggerExitEvent> _playerOnTrigger = null;
    private SceneData _sceneData = null;

    public void Run()
    {
        foreach (int index in _playerOnTrigger)
        {
            ref var entity = ref _playerOnTrigger.GetEntity(index);
            var triggerEvent = entity.Get<OnTriggerExitEvent>();
            if (triggerEvent.Collider.CompareTag(_sceneData.asset.UnloadingZone)) { }
        }
    }
}
