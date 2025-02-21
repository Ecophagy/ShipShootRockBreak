using Microsoft.Xna.Framework.Graphics;

namespace ShipShootRockBreak.Components;

public class RenderComponent(Texture2D texture)
{
    public Texture2D Texture { get; set; } = texture;
}