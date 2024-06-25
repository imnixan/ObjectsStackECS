using Leopotam.Ecs;

public class PositionMonoLink : MonoLink<Position>
{
    public bool ConvertScenePositionToThis = true;

    public override void Make(ref EcsEntity entity)
    {
        if (ConvertScenePositionToThis)
        {
            entity.Get<Position>() = new Position { Value = transform.position };
        }
        else
        {
            base.Make(ref entity);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (ConvertScenePositionToThis)
        {
            Value.Value = transform.position;
        }
    }
#endif
}
