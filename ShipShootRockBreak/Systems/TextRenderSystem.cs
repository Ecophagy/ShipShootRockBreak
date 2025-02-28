using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class TextRenderSystem : IRenderSystem
{
    public void Draw(SpriteBatch spriteBatch, TextRenderComponent renderText, PositionComponent positionComponent)
    {

    }

    public void Draw(SpriteBatch spriteBatch, ComponentManager componentManager)
    {
        foreach (var (entityId, component) in componentManager.TextComponents)
        {
            if (componentManager.VisibleComponents.ContainsKey(entityId))
            {

                spriteBatch.DrawString(component.Font,
                    $"{component.BaseText} {component.Text}",
                    componentManager.PositionComponents[entityId].Position,
                    Color.White);
            }
        }
    }
}