﻿using Leopotam.Ecs;

public abstract class PhysicsLinkBase : MonoLinkBase
{
    protected EcsEntity _entity;

    public override void Make(ref EcsEntity entity)
    {
        _entity = entity;
    }
}
