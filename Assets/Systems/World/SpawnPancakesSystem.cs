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
        var spawnPoint = _sceneData.PancakesSpawnPoint;
        int pancakesCreated = spawnPoint.childCount;
        int maxPankaces = _sceneData.Asset.MaxPancakesSpawn;
        if (_lastTime > _spawnDelay && pancakesCreated < maxPankaces)
        {
            var range = _sceneData.Asset.PancakesSpawnRange;
            _world.NewEntity().Get<SpawnPrefab>() = new SpawnPrefab
            {
                Prefab = _prefab,
                Position =
                    spawnPoint.position
                    + new Vector3(
                        Utils.GetRandomInMinMaxRange(range.min, range.max),
                        0.5f,
                        Utils.GetRandomInMinMaxRange(range.min, range.max)
                    ),
                Rotation = Quaternion.identity,
                Parent = spawnPoint
            };
            _lastTime -= _spawnDelay;

            _sceneData.spawnEffect.Play();
        }
    }
}
