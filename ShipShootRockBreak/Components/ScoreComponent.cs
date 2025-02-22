namespace ShipShootRockBreak.Components;

// The points any entity with this component is worth when killed
public class ScoreComponent(int score)
{
    public int Score { get; } = score;
}