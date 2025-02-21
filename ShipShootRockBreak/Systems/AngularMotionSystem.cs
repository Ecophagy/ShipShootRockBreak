using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class AngularMotionSystem
{
    public void Update(GameTime gameTime, Dictionary<Guid, AngularMotionComponent> motionComponents, Dictionary<Guid, RotationComponent> rotationComponents)
    {
        foreach ( var (entityId, motionComponent) in motionComponents)
        {
            rotationComponents[entityId].Rotation += motionComponent.AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotationComponents[entityId].Rotation %= MathHelper.TwoPi;
        }
    }
}