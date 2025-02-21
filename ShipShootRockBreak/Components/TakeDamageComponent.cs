namespace ShipShootRockBreak.Components;

public class TakeDamageComponent(int startingHealth)
{
    public int Health { get; set; } = startingHealth;
}