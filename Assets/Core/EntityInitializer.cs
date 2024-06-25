using Leopotam.Ecs;
using UnityEngine;

public static class EntityInitializer
{
    public static void InitializeEntities(EcsWorld world)
    {
        var playerMonoLink = Object.FindObjectOfType<PlayerTagMonoLink>();
        var joystickMonoLink = Object.FindObjectOfType<JoystickMonoLink>();

        if (playerMonoLink != null)
        {
            var playerEntity = world.NewEntity();
            playerMonoLink.Make(ref playerEntity);
        }
        else
        {
            Debug.LogWarning("PlayerMonoLink not found!");
        }

        if (joystickMonoLink != null)
        {
            var joystickEntity = world.NewEntity();
            joystickMonoLink.Make(ref joystickEntity);
        }
        else
        {
            Debug.LogWarning("JoystickMonoLink not found!");
        }
    }
}
