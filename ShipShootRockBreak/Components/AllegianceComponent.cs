using ShipShootRockBreak.Constants;

namespace ShipShootRockBreak.Components;
    
// Allegiance is from the point of view of the player
public class AllegianceComponent(Allegiance allegiance)
{
    public Allegiance Allegiance { get; } = allegiance;
}