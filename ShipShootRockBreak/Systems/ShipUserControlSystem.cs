using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ShipUserControlSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        // FIXME: This wants to iterate over a "user controlled" component or something
        foreach (var (entityId, angularMotionComponent) in componentManager.AngularMotionComponents)
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
}