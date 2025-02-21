using System;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public class DeathSystem
{
    public void Update(TakeDamageComponent component)
    {
        if (component.Health <= 0)
        {
            //TODO: DEATH
            Console.Out.WriteLine("DEAD");
        }
    }
}