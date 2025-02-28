using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class GameOverSystem : ISystem
{
    public void Update(Entity ship, Entity gameOver, Dictionary<Guid, DeadComponent> deadComponents, Dictionary<Guid, VisibleComponent> visibleComponents)
    {

    }

    // FIXME: Special update()
    public void Update(GameTime gameTime, ComponentManager componentManager, Entity ship, Entity gameOver)
    {
        if (componentManager.DeadComponents.ContainsKey(ship.Id) && !componentManager.VisibleComponents.ContainsKey(gameOver.Id))
        {
            componentManager.VisibleComponents.Add(gameOver.Id, new VisibleComponent());
        }
    }

    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        throw new NotImplementedException();
    }
}