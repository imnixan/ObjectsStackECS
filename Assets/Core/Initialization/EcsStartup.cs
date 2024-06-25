using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField]
        private SceneData sceneData;

        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        EcsSystems _playerSystems;
        EcsSystems _animationSystems;
        EcsSystems _moveSystems;
        EcsSystems _stackAddSystem;
        EcsSystems _stackRemoveSystem;

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
            _playerSystems = new EcsSystems(_world)
                .Add(new PlayerInputSystem())
                .Add(new CameraFollowPlayerSystem())
                .Add(new TryMoveToUnloadingZone())
                .Add(new TryLeaveUnloadinZone())
                .Add(new TryPickupItem())
                .OneFrame<OnTriggerEnterEvent>()
                .OneFrame<OnTriggerExitEvent>()
                .OneFrame<OnCollisionEnterEvent>();

            _animationSystems = new EcsSystems(_world)
                .Add(new AnimationMoveSystem())
                .Add(new AnimationIdleSystem())
                .Add(new AnimationPlaySystem());

            _moveSystems = new EcsSystems(_world)
                .Add(new MoveRigidbodySystem())
                .Add(new RotateRigidbodySystem())
                .Add(new MoveTransformSystem())
                .Add(new RotateTransformSystem());
        }

        private void InitSystems()
        {
            _updateSystems.Inject(sceneData).Add(_playerSystems).Add(new DestroySystem()).Init();
            _fixedUpdateSystems
                .Inject(sceneData)
                .Add(_moveSystems)
                .Add(_animationSystems)
                .OneFrame<MoveTag>()
                .OneFrame<RotateTag>()
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

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
            }
        }
    }
}
