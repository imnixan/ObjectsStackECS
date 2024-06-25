using Leopotam.Ecs;
using UnityEngine;

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
            var speed = player.Get<Speed>();

            Vector3 direction = new Vector3(joystick.Value.Horizontal, 0, joystick.Value.Vertical);
            if (direction.sqrMagnitude > 0.01f)
            {
                rotation.World = Quaternion.LookRotation(direction);
            }

            position.World += direction * (speed.Value * Time.deltaTime);
        }
    }
}
