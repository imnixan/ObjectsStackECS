using System;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine;

public class UnloadSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world = null;

    private EcsFilter<PlayerTag, StackHolder, OnUnloadTag> _unloadingPlayersFilter;

    public void Run()
    {
        if (_unloadingPlayersFilter.IsEmpty())
        {
            return;
        }
        foreach (int index in _unloadingPlayersFilter)
        {
            ref var entity = ref _unloadingPlayersFilter.GetEntity(index);
            ref var stackHolder = ref entity.Get<StackHolder>();
            ref var lastTime = ref stackHolder.LastUnloadTime;
            ref var unloadDelay = ref stackHolder.UnloadDelay;

            lastTime += Time.deltaTime;
            if (lastTime > unloadDelay && stackHolder.CurrentStack > 0)
            {
                stackHolder.CurrentStack--;

                ref var removeEvent = ref entity.Get<StackRemoveEvent>();
                removeEvent.count++;
                lastTime -= unloadDelay;
            }
        }
    }
}
