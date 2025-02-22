using System;
using System.Collections.Generic;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ScoreboardUpdateSystem
{
    public void Update(Dictionary<Guid, TotalScoreComponent> totalScoreComponents, Dictionary<Guid, TextRenderComponent> textRenderComponents)
    {
        foreach (var (entityId, totalScoreComponent) in totalScoreComponents)
        {
            textRenderComponents[entityId].Text = $"{totalScoreComponent.TotalScore}";
        }
    }
}