using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PrefabFactory : MonoBehaviour
{
    private EcsWorld _world;

    public void SetWorld(EcsWorld world)
    {
        _world = world;
    }

    public void Spawn(SpawnPrefab spawnData)
    {
        GameObject gameObject = Instantiate(
            spawnData.Prefab,
            spawnData.Position,
            spawnData.Rotation,
            spawnData.Parent
        );
        var monoEntity = gameObject.GetComponent<MonoEntity>();
        if (monoEntity == null)
            return;
        var monoLinkComponents = monoEntity.GetComponentsInChildren<MonoLinkBase>();

        EcsEntity ecsEntity = _world.NewEntity();
        foreach (var component in monoLinkComponents)
        {
            component.Make(ref ecsEntity);
        }
    }
}
