using System.Diagnostics;
using Leopotam.Ecs;

public class JoystickMonoLink : MonoLink<JoystickLink>
{
    public bool ConvertScenePositionToThis = true;
    private Joystick joystick;

    public override void Make(ref EcsEntity entity)
    {
        joystick = GetComponent<Joystick>();
        if (ConvertScenePositionToThis)
        {
            entity.Get<JoystickLink>() = new JoystickLink { Value = joystick };
        }
        else
        {
            base.Make(ref entity);
        }
    }
}
