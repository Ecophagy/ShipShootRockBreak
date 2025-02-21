using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ShipUserControlSystem
{
    public void Update(AngularMotionComponent angularMotionComponent)
    {
        // FIXME: Maybe move the keyboard checks into a separate system
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            angularMotionComponent.AngularVelocity = -angularMotionComponent.AngularSpeed;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            angularMotionComponent.AngularVelocity = angularMotionComponent.AngularSpeed;
        }
        else
        {
            angularMotionComponent.AngularVelocity = 0;
        }
    }
}