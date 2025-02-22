using Microsoft.Xna.Framework;

namespace ShipShootRockBreak.Constants;

public struct GameConstants
{
    // Ship
    public const int ShipMaxHealth = 100;
    public static readonly float ShipRotationSpeed = MathHelper.ToRadians(135);

    // Bullet
    public const float BulletCreationThrottle = 0.25f;
    public const int BulletSpeed = 50;
    public const int SpawnDistanceFromShip = 16;
        
    // Asteroid
    public const float AsteroidCreationThrottle = 1f;
    
    // Scoreboard
    public static readonly Vector2 ScoreboardPosition = new(10, 0);
}