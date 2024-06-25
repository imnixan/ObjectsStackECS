using Leopotam.Ecs;
using UnityEngine;

public class AnimationIdleSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<AnimationLink> _filter = null;

    private const string IDLE_ANIM = "Idle";

    public void Init()
    {
        foreach (int index in _filter)
        {
            ref var animator = ref _filter.Get1(index);
            var clip = _sceneData.asset.idleAnim;
            clip.legacy = true;
            clip.wrapMode = WrapMode.Loop;
            animator.Value.AddClip(clip, IDLE_ANIM);
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
            if (animator.NextAnimation == "")
            {
                if (animator.CurrentAnimation == IDLE_ANIM)
                {
                    return;
                }
                animator.CurrentAnimation = IDLE_ANIM;
            }
            else
            {
                animator.CurrentAnimation = animator.NextAnimation;
                animator.NextAnimation = "";
            }
        }
    }
}
