using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerExitMonoLink : PhysicsLinkBase
{
    private void OnTriggerExit(Collider other)
    {
        _entity.Get<OnTriggerExitEvent>() = new OnTriggerExitEvent()
        {
            Collider = other,
            Sender = gameObject
        };
    }
}
