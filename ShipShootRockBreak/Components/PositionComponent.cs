using Microsoft.Xna.Framework;

namespace ShipShootRockBreak.Components;

public class PositionComponent(Vector2 startingPosition)
{
    public Vector2 Position { get; set; } = startingPosition;
}