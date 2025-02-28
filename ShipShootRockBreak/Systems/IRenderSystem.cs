using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public interface IRenderSystem
{
    public void Draw(SpriteBatch spriteBatch, ComponentManager componentManager);
}