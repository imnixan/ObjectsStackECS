using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif
            EntityInitializer.InitializeEntities(_world);
            _updateSystems.Init();

            _fixedUpdateSystems
                .Add(new PlayerInputSystem())
                .Add(new UpdateRigidbodySystem())
                .Init();
        }

        void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}
