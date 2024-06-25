using Leopotam.Ecs;

public class GameObjectMonoLink : MonoLink<GameObjectLink>
{
    public override void Make(ref EcsEntity entity)
    {
        entity.Get<GameObjectLink>() = new GameObjectLink { Value = gameObject };
    }
}
