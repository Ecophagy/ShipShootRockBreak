using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class LinearMotionSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach ( var (entityId, motionComponent) in componentManager.MotionComponents)
        {
            componentManager.PositionComponents[entityId].Position += motionComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}