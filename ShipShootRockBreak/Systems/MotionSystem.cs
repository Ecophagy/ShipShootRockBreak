using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class MotionSystem
{
    public void Update(MotionComponent motion, PositionComponent position)
    {
        position.Position += motion.Velocity;
    }
}