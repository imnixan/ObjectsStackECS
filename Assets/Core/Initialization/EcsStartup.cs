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
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems);

#endif
            EntityInitializer.InitializeEntities(_world);

            _updateSystems
                .Add(new PlayerInputSystem())
                .Add(new CameraFollowPlayerSystem())
                //.InjectUi(sceneData.uiEmitter)
                //.OneFrame<EcsUiDragEvent>()
                .Inject(sceneData)
                .Init();

            _fixedUpdateSystems
                .Add(new MoveRigidbodySystem())
                .Add(new RotateRigidbodySystem())
                .Add(new MoveTransformSystem())
                .Add(new RotateTransformSystem())
                .Add(new AnimationMoveSystem())
                .Add(new AnimationIdleSystem())
                .Add(new AnimationPlaySystem())
                .OneFrame<MoveTag>()
                .OneFrame<RotateTag>()
                .Inject(sceneData)
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
