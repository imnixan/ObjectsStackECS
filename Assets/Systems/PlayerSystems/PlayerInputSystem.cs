using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;

    private EcsFilter<PlayerTag, Position, Speed, Rotation> _playerFilter = null;

    private EcsFilter<JoystickLink> _joystickFilter = null;

    public void Run()
    {
        if (_playerFilter.IsEmpty() || _joystickFilter.IsEmpty())
            return;

        var joystick = _joystickFilter.Get1(0);
        foreach (int index in _playerFilter)
        {
            EcsEntity player = _playerFilter.GetEntity(index);
            ref var position = ref player.Get<Position>();
            ref var rotation = ref player.Get<Rotation>();
            ref var speed = ref player.Get<Speed>();

            Vector3 direction = new Vector3(joystick.Value.Horizontal, 0, joystick.Value.Vertical);

            var directionMagnitude = direction.sqrMagnitude;
            speed.CurrentSpeed = speed.MaxSpeed * directionMagnitude;
            if (directionMagnitude > 0.01f)
            {
                Quaternion newRotation = Quaternion.LookRotation(direction);
                if (newRotation != rotation.World)
                {
                    player.Get<RotateTag>();
                    rotation.World = newRotation;
                }
            }

            Vector3 newPosition =
                position.World + direction * (speed.CurrentSpeed * Time.deltaTime);
            if (directionMagnitude > 0.01f)
            {
                player.Get<MoveTag>();
                position.World += direction * (speed.CurrentSpeed * Time.deltaTime);
            }
        }
    }
}
