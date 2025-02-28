using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ScoreSystem : ISystem
{
    public void Update(Dictionary<Guid, ScoreComponent> scoreComponents, Dictionary<Guid, DeadComponent> deadComponents, Dictionary<Guid, TotalScoreComponent> totalComponents)
    {

    }

    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach (var (entityId, scoreComponent) in componentManager.ScoreComponents)
        {
            if (componentManager.DeadComponents.ContainsKey(entityId))
            {
                foreach (var component in componentManager.TotalScoreComponents)
                {
                    component.Value.TotalScore += scoreComponent.Score;
                }
            }
        }
    }
}