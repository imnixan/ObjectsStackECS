using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        [SerializeField]
        private SceneData sceneData;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world, "World");
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);

#endif
            EntityInitializer.InitializeEntities(_world);

            BuildSystems();
            InitSystems();
        }

        private void BuildSystems()
        {
            var playerSystems = new EcsSystems(_world)
                .Add(new PlayerInputSystem())
                .Add(new CameraFollowPlayerSystem());

            var animationSystems = new EcsSystems(_world)
                .Add(new AnimationMoveSystem())
                .Add(new AnimationIdleSystem())
                .Add(new AnimationPlaySystem());

            var moveSystems = new EcsSystems(_world)
                .Add(new MoveRigidbodySystem())
                .Add(new RotateRigidbodySystem())
                .Add(new MoveTransformSystem())
                .Add(new RotateTransformSystem());

            _updateSystems.Inject(sceneData).Add(playerSystems);
            _fixedUpdateSystems
                .Inject(sceneData)
                .Add(moveSystems)
                .Add(animationSystems)
                .OneFrame<MoveTag>()
                .OneFrame<RotateTag>();
        }

        private void InitSystems()
        {
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
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

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
            }
        }
    }
}
