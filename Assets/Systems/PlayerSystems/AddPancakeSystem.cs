using Leopotam.Ecs;
using UnityEngine;

public class AddPancakeSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world = null;

    private EcsFilter<PlayerTag, Pan, StackAddEvent> _playersFilter = null;
    private GameObject _prefab;

    public void Init()
    {
        _prefab = _sceneData.Asset.PancakeStackPrefab;
    }

    public void Run()
    {
        if (_playersFilter.IsEmpty())
        {
            return;
        }
        foreach (int index in _playersFilter)
        {
            ref var entity = ref _playersFilter.GetEntity(index);
            var pickupCount = entity.Get<StackAddEvent>().count;
            var pan = entity.Get<Pan>().pan;
            float yPos = 0.5f + pan.childCount * 0.1f;
            Debug.Log("Start placing pancakes");
            for (int i = 0; i < pickupCount; i++)
            {
                _world.NewEntity().Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Prefab = _prefab,
                    Position = pan.position + new Vector3(0, yPos + yPos * i, 0),
                    Rotation = Quaternion.identity,
                    Parent = pan
                };
                Debug.Log("Placed" + i + 1);
            }
        }
    }
}
