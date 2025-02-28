using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class AngularMotionSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach (var (entityId, motionComponent) in componentManager.AngularMotionComponents)
        {
            componentManager.RotationComponents[entityId].Rotation += motionComponent.AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            componentManager.RotationComponents[entityId].Rotation %= MathHelper.TwoPi;
        }
    }
}