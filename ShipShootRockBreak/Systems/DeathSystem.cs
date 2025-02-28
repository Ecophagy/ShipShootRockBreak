using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DeathSystem : ISystem
{
    public void Update(Guid entityId,
        Dictionary<Guid, RenderComponent> renderComponents,
        Dictionary<Guid, PositionComponent> positionComponents,
        Dictionary<Guid, LinearMotionComponent> motionComponents,
        Dictionary<Guid, CollisionComponent> collisionComponents,
        Dictionary<Guid, DealDamageComponent> dealDamageComponents,
        Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
        Dictionary<Guid, DeadComponent> deadComponents)
    {

    }

    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach (var (entityId, _) in componentManager.DeadComponents)
        {
            // TODO: Can we make "remove all components associated with an entity" a function in the component manager?
            componentManager.RenderComponents.Remove(entityId);
            componentManager.PositionComponents.Remove(entityId);
            componentManager.MotionComponents.Remove(entityId);
            componentManager.CollisionComponents.Remove(entityId);
            componentManager.DealDamageComponents.Remove(entityId);
            componentManager.TakeDamageComponents.Remove(entityId);
            componentManager.DeadComponents.Remove(entityId);
            componentManager.PlayerComponents.Remove(entityId);
        }
    }
}