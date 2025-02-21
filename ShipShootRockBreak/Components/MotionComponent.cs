using Microsoft.Xna.Framework;

namespace ShipShootRockBreak.Components;

public class MotionComponent(Vector2 initialVelocity)
{
    public Vector2 Velocity { get; set; } = initialVelocity;
}