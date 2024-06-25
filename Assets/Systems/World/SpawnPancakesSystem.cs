using Leopotam.Ecs;
using UnityEngine;

public class SpawnPancakesSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world = null;

    private float _spawnDelay;
    private float _lastTime;
    private GameObject _prefab;

    public void Init()
    {
        _spawnDelay = _sceneData.Asset.SpawnTimer;
        _prefab = _sceneData.Asset.PancakePrefab;
    }

    public void Run()
    {
        _lastTime += Time.deltaTime;
        if (_lastTime > _spawnDelay)
        {
            var xRange = _sceneData.Asset.PancakesXSpawnRange;
            var zRange = _sceneData.Asset.PancakesZSpawnRange;
            _world.NewEntity().Get<SpawnPrefab>() = new SpawnPrefab
            {
                Prefab = _prefab,
                Position = new Vector3(
                    Random.Range(xRange.min, xRange.max),
                    0.5f,
                    Random.Range(zRange.min, zRange.max)
                ),
                Rotation = Quaternion.identity,
                Parent = _sceneData.PancakesParent
            };
            _lastTime -= _spawnDelay;
        }
    }
}
