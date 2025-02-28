using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DamageSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach (var (entityId1, dealDamage) in componentManager.DealDamageComponents)
        {
            foreach (var (entityId2, takeDamage) in componentManager.TakeDamageComponents)
            {
                // Do not damage self and do not damage same allegiance
                // TODO: Better filtering to avoid this just in time check?
                if (entityId1 != entityId2 && componentManager.AllegianceComponents[entityId1].Allegiance != componentManager.AllegianceComponents[entityId2].Allegiance) 
                {
                    var rect1 = new Rectangle((int)componentManager.PositionComponents[entityId1].Position.X,
                        (int)componentManager.PositionComponents[entityId1].Position.Y,
                        componentManager.CollisionComponents[entityId1].Width,
                        componentManager.CollisionComponents[entityId1].Height);
                    var rect2 = new Rectangle((int)componentManager.PositionComponents[entityId2].Position.X,
                        (int)componentManager.PositionComponents[entityId2].Position.Y,
                        componentManager.CollisionComponents[entityId2].Width,
                        componentManager.CollisionComponents[entityId2].Height);

                    if (rect1.Intersects(rect2))
                    {
                        componentManager.TakeDamageComponents[entityId2].Health -= componentManager.DealDamageComponents[entityId1].Damage;
                        if (componentManager.TakeDamageComponents[entityId2].Health <= 0)
                        {
                            componentManager.DeadComponents.Add(entityId2, new DeadComponent());
                        }
                    }
                }
            }
        }
    }
}