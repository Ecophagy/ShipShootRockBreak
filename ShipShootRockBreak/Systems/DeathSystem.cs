using System;
using System.Collections.Generic;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DeathSystem
{
    public void Update(Guid entityId,
        Dictionary<Guid, RenderComponent> renderComponents,
        Dictionary<Guid, PositionComponent> positionComponents,
        Dictionary<Guid, MotionComponent> motionComponents,
        Dictionary<Guid, CollisionComponent> collisionComponents,
        Dictionary<Guid, DealDamageComponent> dealDamageComponents,
        Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
        Dictionary<Guid, DeadComponent> deadComponents)
    {
            renderComponents.Remove(entityId);
            positionComponents.Remove(entityId);
            motionComponents.Remove(entityId);
            collisionComponents.Remove(entityId);
            dealDamageComponents.Remove(entityId);
            takeDamageComponents.Remove(entityId);
            deadComponents.Remove(entityId);
    }
}