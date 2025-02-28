using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class ScoreboardUpdateSystem : ISystem
{
    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        foreach (var (entityId, totalScoreComponent) in componentManager.TotalScoreComponents)
        {
            componentManager.TextComponents[entityId].Text = $"{totalScoreComponent.TotalScore}";
        }
    }
}