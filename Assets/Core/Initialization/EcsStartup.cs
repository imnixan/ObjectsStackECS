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

        [SerializeField]
        private UIData uiData;

        EcsWorld _world;
        EcsSystems _updateSystems;
        EcsSystems _fixedUpdateSystems;

        EcsSystems _playerSystems;
        EcsSystems _animationSystems;
        EcsSystems _moveSystems;
        EcsSystems _stackAddSystem;
        EcsSystems _stackRemoveSystem;
        EcsSystems _worldSystem;
        EcsSystems _uiSystem;

        void Start()
        {
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
            _worldSystem = new EcsSystems(_world)
                .Add(new SpawnSystem())
                .Add(new SpawnPancakesSystem());

            _playerSystems = new EcsSystems(_world)
                .Add(new CameraFollowPlayerSystem())
                .Add(new TryMoveToUnloadingZone())
                .Add(new TryLeaveUnloadinZone())
                .Add(new TryPickupItem())
                .Add(new AddPancakeSystem())
                .OneFrame<StackAddEvent>()
                .Add(new UnloadSystem())
                .Add(new RemovePancakeSystem())
                .OneFrame<StackRemoveEvent>()
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

            _uiSystem = new EcsSystems(_world).Inject(uiData).Add(new UiStackCountSystem());
        }

        private void InitSystems()
        {
            _updateSystems
                .Inject(sceneData)
                .Add(_worldSystem)
                .Add(_playerSystems)
                .Add(new DestroySystem())
                .Add(_uiSystem)
                .Init();

            _fixedUpdateSystems
                .Inject(sceneData)
                .Add(new PlayerInputSystem())
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
