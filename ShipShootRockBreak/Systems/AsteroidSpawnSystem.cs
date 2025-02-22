using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class AsteroidSpawnSystem(float creationThrottle)
{
    private float CreationThrottle { get; } = creationThrottle;
    private float Timer { get; set; }
    private const int AsteroidSpeed = 25;
    private Random _random { get; } = new Random();

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
        if (Timer >= CreationThrottle)
        {
            var shipLocation = positionComponents[shipId];
            
            var side = _random.Next(0, 4);
            Vector2 asteroidPosition = new Vector2();
            switch (side)
            {
                case 0:
                    asteroidPosition = new Vector2(_random.Next(0, Game1.ScreenWidth), 0);
                    break;
                case 1:
                    asteroidPosition = new Vector2(Game1.ScreenWidth, _random.Next(0, Game1.ScreenHeight));
                    break;
                case 2:
                    asteroidPosition =  new Vector2(_random.Next(0, Game1.ScreenWidth), Game1.ScreenHeight);
                    break;
                case 3:
                    asteroidPosition = new Vector2(0, _random.Next(0, Game1.ScreenHeight));
                    break;
            }

            var relativePosition = shipLocation.Position - asteroidPosition;
            var relativeAngle = Math.Atan2(relativePosition.Y, relativePosition.X);
            var asteroidVelocity = new Vector2((float)(Math.Cos(relativeAngle) * AsteroidSpeed), (float)(Math.Sin(relativeAngle) * AsteroidSpeed));

            
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