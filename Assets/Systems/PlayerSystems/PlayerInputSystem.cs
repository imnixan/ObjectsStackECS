using System.Linq.Expressions;
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

        // Получаем позицию мыши на экране и вычисляем направление взгляда
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 lookDirection = new Vector3();

        // Проверяем, попал ли луч в поверхность

        bool raycastSucces = false;
        Vector3 mousePosition = new Vector3();

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            raycastSucces = true;
            mousePosition = hitInfo.point;
        }

        foreach (int index in _playerFilter)
        {
            EcsEntity player = _playerFilter.GetEntity(index);
            ref var position = ref player.Get<Position>();
            ref var rotation = ref player.Get<Rotation>();
            ref var speed = ref player.Get<Speed>();

            if (raycastSucces)
            {
                lookDirection = mousePosition - position.World; // Получаем позицию игрока
                lookDirection.y = 0; // Игнорируем ось Y, чтобы игрок не смотрел вверх или вниз
            }

            // Если луч попал, обновляем направление взгляда
            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion newRotation = Quaternion.LookRotation(lookDirection);
                if (newRotation != rotation.World)
                {
                    player.Get<RotateTag>();
                    rotation.World = newRotation;
                }
            }

            // Определяем направления вперёд, назад, влево и вправо относительно взгляда игрока
            Vector3 forward = rotation.World * Vector3.forward;
            Vector3 right = rotation.World * Vector3.right;

            // Ввод WASD для направления движения относительно взгляда
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                direction += forward; // Вперёд относительно взгляда
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction -= forward; // Назад относительно взгляда
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction -= right; // Влево относительно взгляда
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += right; // Вправо относительно взгляда
            }

            // Нормализация направления и расчёт скорости
            if (direction.sqrMagnitude > 0.01f)
            {
                direction.Normalize();
                float directionMagnitude = direction.sqrMagnitude;
                speed.CurrentSpeed = speed.MaxSpeed * Mathf.Clamp01(directionMagnitude);

                // Обновление позиции игрока
                Vector3 newPosition =
                    position.World + direction * (speed.CurrentSpeed * Time.deltaTime);
                player.Get<MoveTag>();
                position.World = newPosition;
            }
        }
    }
}
