using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class AsteroidSpawnSystem()
{
    private float Timer { get; set; }
    private Random Random { get; } = new();

    public void Update(GameTime gameTime,
        AsteroidFactory asteroidFactory,
        Guid shipId,
        Dictionary<Guid, RenderComponent> renderComponents,
        Dictionary<Guid, PositionComponent> positionComponents,
        Dictionary<Guid, RotationComponent> rotationComponents,
        Dictionary<Guid, LinearMotionComponent> linearMotionComponents,
        Dictionary<Guid, CollisionComponent> collisionComponents,
        Dictionary<Guid, DealDamageComponent> dealDamageComponents,
        Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
        Dictionary<Guid, AllegianceComponent> allegianceComponents,
        Dictionary<Guid, ScoreComponent> scoreComponents)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Timer >= GameConstants.AsteroidCreationThrottle)
        {
            var shipLocation = positionComponents[shipId];
            
            var side = Random.Next(0, 4);
            var asteroidPosition = side switch
            {
                0 => new Vector2(Random.Next(0, Game1.ScreenWidth), 0),
                1 => new Vector2(Game1.ScreenWidth, Random.Next(0, Game1.ScreenHeight)),
                2 => new Vector2(Random.Next(0, Game1.ScreenWidth), Game1.ScreenHeight),
                3 => new Vector2(0, Random.Next(0, Game1.ScreenHeight)),
                _ => new Vector2()
            };

            var relativePosition = shipLocation.Position - asteroidPosition;
            var relativeAngle = Math.Atan2(relativePosition.Y, relativePosition.X);
            var asteroidVelocity = new Vector2((float)(Math.Cos(relativeAngle) * GameConstants.BulletSpeed), (float)(Math.Sin(relativeAngle) * GameConstants.BulletSpeed));
            
            asteroidFactory.CreateAsteroid(renderComponents,
                positionComponents,
                rotationComponents,
                linearMotionComponents,
                collisionComponents,
                dealDamageComponents,
                takeDamageComponents,
                allegianceComponents,
                scoreComponents,
                asteroidPosition,
                asteroidVelocity);

            Timer = 0;
        }
    }
    
}