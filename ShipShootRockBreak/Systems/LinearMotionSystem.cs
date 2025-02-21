using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class LinearMotionSystem
{
    public void Update(GameTime gameTime, Dictionary<Guid, LinearMotionComponent> motionComponents, Dictionary<Guid, PositionComponent> positionComponents)
    {
        foreach ( var (entityId, motionComponent) in motionComponents)
        {
            positionComponents[entityId].Position += motionComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}