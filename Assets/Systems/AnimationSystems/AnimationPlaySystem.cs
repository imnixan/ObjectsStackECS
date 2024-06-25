using Leopotam.Ecs;
using UnityEngine;

public class AnimationPlaySystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<AnimationLink> _filter = null;

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
            if (!animator.Value.IsPlaying(animator.CurrentAnimation))
            {
                animator.Value.Play(animator.CurrentAnimation);
            }
        }
    }
}
