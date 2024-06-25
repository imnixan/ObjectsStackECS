//using Leopotam.Ecs;
//using UnityEngine;

//public class PlayerInputSystem : IEcsRunSystem
//{
//    private EcsWorld _world;
//    private SceneData _sceneData;

//    private EcsFilter<PlayerTag, Position, Speed> _playerFilter = null;

//    //private EcsFilter<JoystickComponent> _joystickFilter = null;

//    public void Run()
//    {
//        if (_playerFilter.IsEmpty() || _joystickFilter.IsEmpty())
//            return;
//        ref var joystick = ref _joystickFilter.Get1(0);
//        foreach (int index in _playerFilter)
//        {
//            var position = _playerFilter.Get2(index);
//            var speed = _playerFilter.Get3(index);

//            Vector3 direction = new Vector3(joystick.Input.x, 0, joystick.Input.y);
//            position.Value += direction * (speed.Value * Time.deltaTime);
//        }
//    }
//}
