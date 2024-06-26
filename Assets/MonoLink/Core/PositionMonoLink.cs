﻿using Leopotam.Ecs;

public class PositionMonoLink : MonoLink<Position>
{
    public bool ConvertScenePositionToThis = true;

    public override void Make(ref EcsEntity entity)
    {
        if (ConvertScenePositionToThis)
        {
            entity.Get<Position>() = new Position
            {
                World = transform.position,
                Local = transform.localPosition
            };
        }
        else
        {
            base.Make(ref entity);
        }
    }
}
