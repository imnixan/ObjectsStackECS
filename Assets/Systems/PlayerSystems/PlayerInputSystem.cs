using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsWorld _world;

    private EcsFilter<PlayerTag, Position, Speed, Rotation> _playerFilter = null;

    public void Run()
    {
        if (_playerFilter.IsEmpty())
            return;

        Vector3 direction = Vector3.zero;

        // Обработка ввода WASD
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        // Получаем позицию мыши на экране и вычисляем направление взгляда
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 mousePosition = hitInfo.point;

            foreach (int index in _playerFilter)
            {
                EcsEntity player = _playerFilter.GetEntity(index);
                ref var position = ref player.Get<Position>();
                ref var rotation = ref player.Get<Rotation>();
                ref var speed = ref player.Get<Speed>();

                // Движение игрока
                float directionMagnitude = direction.sqrMagnitude;
                speed.CurrentSpeed = speed.MaxSpeed * Mathf.Clamp01(directionMagnitude);

                if (directionMagnitude > 0.01f)
                {
                    Vector3 newPosition =
                        position.World
                        + direction.normalized * (speed.CurrentSpeed * Time.deltaTime);
                    player.Get<MoveTag>();
                    position.World = newPosition;
                }

                // Направление взгляда в сторону курсора мыши
                Vector3 lookDirection = mousePosition - position.World;
                lookDirection.y = 0; // Игнорируем ось Y, чтобы игрок не смотрел вверх или вниз
                if (lookDirection.sqrMagnitude > 0.01f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(lookDirection);
                    if (newRotation != rotation.World)
                    {
                        player.Get<RotateTag>();
                        rotation.World = newRotation;
                    }
                }
            }
        }
    }
}
