using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class RenderSystem
{
    public void Draw(SpriteBatch spriteBatch, RenderComponent renderComponent, PositionComponent positionComponent, RotationComponent rotationComponent)
    {
        spriteBatch.Draw(renderComponent.Texture, 
            positionComponent.Position,
            null,
            Color.White,
            rotationComponent.Rotation,
            renderComponent.SpriteOrigin,
            1.0f,
            SpriteEffects.None, 
            0f);
    }
}