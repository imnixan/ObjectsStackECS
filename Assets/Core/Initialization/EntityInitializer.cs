using Leopotam.Ecs;
using UnityEngine;

public static class EntityInitializer
{
    public static void InitializeEntities(EcsWorld world)
    {
        InitializeEntity<PlayerTagMonoLink>(world);
        InitializeEntity<JoystickMonoLink>(world);
        InitializeEntity<CameraTagMonoLink>(world);
    }

    private static void InitializeEntity<T>(EcsWorld world)
        where T : MonoLinkBase
    {
        var monoLinks = Object.FindObjectsOfType<T>();

        foreach (var monoLink in monoLinks)
        {
            var entity = world.NewEntity();
            var monoLinkComponents = monoLink.GetComponents<MonoLinkBase>();

            foreach (var component in monoLinkComponents)
            {
                component.Make(ref entity);
            }
        }
    }
}
