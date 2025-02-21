using System;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class CollisionSystem
{
    public void Update(CollisionComponent collision1, PositionComponent position1, CollisionComponent collision2, PositionComponent position2)
    {
        var rect1 = new Rectangle((int)position1.Position.X, (int)position1.Position.Y, collision1.Width, collision1.Height);
        var rect2 = new Rectangle((int)position2.Position.X, (int)position2.Position.Y, collision2.Width, collision2.Height);
        
        if (rect1.Intersects(rect2))
        {
            Console.Out.WriteLine("Collision!");
        }

    }
}