using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class RenderSystem
{
    public void Draw(SpriteBatch spriteBatch, RenderComponent renderComponent, PositionComponent positionComponent)
    {
        spriteBatch.Draw(renderComponent.Texture, positionComponent.Position, Color.White);
    }
}