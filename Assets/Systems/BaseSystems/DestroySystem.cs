using Leopotam.Ecs;
using UnityEngine;

public class DestroySystem : IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;

    private EcsFilter<DestroyTag> _destroyFilter = null;

    private PrefabFactory _factory;

    public void Run()
    {
        if (_destroyFilter.IsEmpty())
        {
            return;
        }

        foreach (int index in _destroyFilter)
        {
            ref EcsEntity destroyEntity = ref _destroyFilter.GetEntity(index);
            GameObject gameObject = destroyEntity.Get<GameObjectLink>().Value;
            GameObject.Destroy(gameObject);
            destroyEntity.Destroy();
        }
    }
}
