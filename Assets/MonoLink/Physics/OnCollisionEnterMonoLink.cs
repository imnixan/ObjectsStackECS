using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnCollisionEnterMonoLink : PhysicsLinkBase
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COllision");
        _entity.Get<OnCollisionEnterEvent>() = new OnCollisionEnterEvent()
        {
            Collision = other,
            Sender = gameObject
        };
    }
}
