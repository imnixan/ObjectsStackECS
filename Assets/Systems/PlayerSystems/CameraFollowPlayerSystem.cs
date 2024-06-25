using Leopotam.Ecs;
using UnityEngine;

public class CameraFollowPlayerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<CameraTag> _cameraFilter = null;
    private EcsFilter<PlayerTag> _playerFilter = null;

    public void Init()
    {
        ref var player = ref _playerFilter.GetEntity(0);
        var playerPosition = player.Get<Position>();
        foreach (int index in _cameraFilter)
        {
            ref var camera = ref _cameraFilter.GetEntity(index);
            var cameraPosition = camera.Get<Position>();
            camera.Get<PosDiff>() = new PosDiff
            {
                Value = cameraPosition.World - playerPosition.World
            };
        }
    }

    public void Run()
    {
        ref var player = ref _playerFilter.GetEntity(0);
        var playerPosition = player.Get<Position>();
        foreach (int index in _cameraFilter)
        {
            ref var camera = ref _cameraFilter.GetEntity(index);
            var posDiff = camera.Get<PosDiff>();
            ref var cameraPosition = ref camera.Get<Position>();

            Vector3 newPos = playerPosition.World + posDiff.Value;
            if (newPos != cameraPosition.World)
            {
                camera.Get<MoveTag>();

                cameraPosition.World = newPos;
            }
        }
    }
}
