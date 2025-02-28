using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class RenderSystem
{
    public void Draw(SpriteBatch spriteBatch, ComponentManager componentManager)
    {
        foreach (var (entityId, renderComponent) in componentManager.RenderComponents)
        {
            spriteBatch.Draw(renderComponent.Texture, 
                componentManager.PositionComponents[entityId].Position,
                null,
                Color.White,
                componentManager.RotationComponents[entityId].Rotation,
                renderComponent.SpriteOrigin,
                1.0f,
                SpriteEffects.None, 
                0f);
        }
        

    }
}