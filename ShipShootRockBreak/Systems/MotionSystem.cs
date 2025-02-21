using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class MotionSystem
{
    public void Update(GameTime gameTime, Dictionary<Guid, MotionComponent> motionComponents, Dictionary<Guid, PositionComponent> positionComponents)
    {
        foreach ( var (entityId, motionComponent) in motionComponents)
        {
            positionComponents[entityId].Position += motionComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}