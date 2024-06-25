using Leopotam.Ecs;
using UnityEngine;

public class AnimationMoveSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<AnimationLink, MoveTag> _filter = null;
    private EcsFilter<AnimationLink> _animatorFilter = null;
    private const string MOVE_ANIM = "Move";

    public void Init()
    {
        foreach (int index in _animatorFilter)
        {
            ref var animator = ref _animatorFilter.Get1(index);
            var clip = _sceneData.asset.moveAnim;
            clip.legacy = true;
            clip.wrapMode = WrapMode.Loop;
            animator.Value.AddClip(clip, MOVE_ANIM);
        }
    }

    public void Run()
    {
        if (_filter.IsEmpty())
        {
            return;
        }

        foreach (int index in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(index);
            ref var animator = ref entity.Get<AnimationLink>();
            var move = entity.Get<MoveTag>();
            var speed = entity.Get<Speed>();
            animator.Value[MOVE_ANIM].speed = speed.CurrentSpeed / speed.MaxSpeed;
            animator.NextAnimation = MOVE_ANIM;
        }
    }
}
