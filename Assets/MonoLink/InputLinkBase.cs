using Leopotam.Ecs;

namespace UnityComponents.MonoLinks.Base
{
    public abstract class InputLinkBase : MonoLinkBase
    {
        protected EcsEntity _entity;

        public override void Make(ref EcsEntity entity)
        {
            _entity = entity;
        }
    }
}
