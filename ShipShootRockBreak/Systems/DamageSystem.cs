using System;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DamageSystem
{
    public void Update(CollisionComponent collision1, 
        PositionComponent position1,
        DealDamageComponent dealDamage1,
        CollisionComponent collision2, 
        PositionComponent position2,
        TakeDamageComponent takeDamage2)
    {
        var rect1 = new Rectangle((int)position1.Position.X, (int)position1.Position.Y, collision1.Width, collision1.Height);
        var rect2 = new Rectangle((int)position2.Position.X, (int)position2.Position.Y, collision2.Width, collision2.Height);
        
        if (rect1.Intersects(rect2))
        {
            takeDamage2.Health -= dealDamage1.Damage;
            Console.Out.WriteLine("Damage!");
        }

    }
}