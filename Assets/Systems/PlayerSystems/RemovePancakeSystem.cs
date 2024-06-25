using Leopotam.Ecs;
using UnityEngine;

public class RemovePancakeSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world = null;

    private EcsFilter<PlayerTag, Pan, StackRemoveEvent> _playersFilter = null;

    public void Run()
    {
        if (_playersFilter.IsEmpty())
        {
            return;
        }
        foreach (int index in _playersFilter)
        {
            ref var entity = ref _playersFilter.GetEntity(index);
            var removeCount = entity.Get<StackRemoveEvent>().count;
            var pan = entity.Get<Pan>().pan;
            float yPos = 0.5f;
            Debug.Log($"remove {removeCount} pancakes in moment");
            for (int i = 0; i < removeCount; i++)
            {
                GameObject.Destroy(pan.GetChild(i).gameObject);
            }
        }
    }
}
