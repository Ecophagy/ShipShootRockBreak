using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class TextRenderSystem
{
    public void Draw(SpriteBatch spriteBatch, TextRenderComponent renderText, PositionComponent positionComponent)
    {
        spriteBatch.DrawString(renderText.Font,
            $"{renderText.BaseText} {renderText.Text}",
            positionComponent.Position,
            Color.White);
    }
}