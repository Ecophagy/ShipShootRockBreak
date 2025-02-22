using System;
using System.Collections.Generic;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ScoreSystem
{
    public void Update(Dictionary<Guid, ScoreComponent> scoreComponents, Dictionary<Guid, DeadComponent> deadComponents, Dictionary<Guid, TotalScoreComponent> totalComponents)
    {
        foreach (var (entityId, scoreComponent) in scoreComponents)
        {
            if (deadComponents.ContainsKey(entityId))
            {
                foreach (var component in totalComponents)
                {
                    component.Value.TotalScore += scoreComponent.Score;
                    Console.Out.WriteLine(component.Value.TotalScore);
                }
            }
        }
    }
}