using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DamageSystem
{
    public void Update(Dictionary<Guid, CollisionComponent> collisionComponents,
        Dictionary<Guid, PositionComponent> positionComponents,
        Dictionary<Guid, DealDamageComponent> dealDamageComponents,
        Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
        Dictionary<Guid, DeadComponent> deadComponents)
    {
        foreach (var (entityId1, dealDamage) in dealDamageComponents)
        {
            foreach (var (entityId2, takeDamage) in takeDamageComponents)
            {
                if (entityId1 != entityId2) // Do not damage self
                {
                    var rect1 = new Rectangle((int)positionComponents[entityId1].Position.X,
                        (int)positionComponents[entityId1].Position.Y,
                        collisionComponents[entityId1].Width,
                        collisionComponents[entityId1].Height);
                    var rect2 = new Rectangle((int)positionComponents[entityId2].Position.X,
                        (int)positionComponents[entityId2].Position.Y,
                        collisionComponents[entityId2].Width,
                        collisionComponents[entityId2].Height);

                    if (rect1.Intersects(rect2))
                    {
                        takeDamageComponents[entityId2].Health -= dealDamageComponents[entityId1].Damage;
                        if (takeDamageComponents[entityId2].Health <= 0)
                        {
                            deadComponents.Add(entityId2, new DeadComponent());
                        }
                    }
                }
            }
        }
    }
}