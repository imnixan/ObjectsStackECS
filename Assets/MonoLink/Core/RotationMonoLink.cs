using Leopotam.Ecs;

public class RotationMonoLink : MonoLink<Rotation>
{
    public bool ConvertScenePositionToThis = true;

    public override void Make(ref EcsEntity entity)
    {
        if (ConvertScenePositionToThis)
        {
            entity.Get<Rotation>() = new Rotation
            {
                World = transform.rotation,
                Local = transform.localRotation
            };
        }
        else
        {
            base.Make(ref entity);
        }
    }
}
