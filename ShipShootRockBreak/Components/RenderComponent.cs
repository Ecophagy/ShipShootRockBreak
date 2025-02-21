using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace ShipShootRockBreak.Components;

public class RenderComponent
{
    public Texture2D Texture { get; set; }
    public Vector2 SpriteOrigin { get; }

    public RenderComponent(Texture2D texture)
    {
        Texture = texture;
        SpriteOrigin = new Vector2(Texture.Width/2, Texture.Height/2);
    }
}