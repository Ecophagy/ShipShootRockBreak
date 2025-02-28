using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class GameOverSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        if (componentManager.PlayerComponents.Count == 0)
        {
            foreach (var (entityId, _) in componentManager.GameOverComponents)
            {
                if (!componentManager.VisibleComponents.ContainsKey(entityId))
                {
                    componentManager.AddVisibleComponent(entityId);
                }
            }
        }
    }
}