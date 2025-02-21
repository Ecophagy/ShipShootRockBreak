using System;
using System.Collections.Generic;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class GameOverSystem
{
    public void Update(Entity ship, Entity gameOver, Dictionary<Guid, DeadComponent> deadComponents, Dictionary<Guid, VisibleComponent> visibleComponents)
    {
        if (deadComponents.ContainsKey(ship.Id) && !visibleComponents.ContainsKey(gameOver.Id))
        {
            visibleComponents.Add(gameOver.Id, new VisibleComponent());
        }
    }
}