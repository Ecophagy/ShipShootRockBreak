using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;

namespace ShipShootRockBreak.Entities;

public class AsteroidFactory(Texture2D texture)
{
    private readonly Texture2D _texture = texture;
    private const int Damage = 100;
    private const int Health = 20;
    private const int Score = 100;

    public void CreateAsteroid(ComponentManager componentManager,
        Vector2 position,
        Vector2 velocity)
    {
        var asteroidEntity = new Entity("asteroid");
        componentManager.AddRenderComponent(asteroidEntity.Id, _texture);
        componentManager.AddPositionComponent(asteroidEntity.Id, position);
        componentManager.AddRotationComponent(asteroidEntity.Id);
        componentManager.AddLinearMotionComponent(asteroidEntity.Id, velocity);
        componentManager.AddCollisionComponent(asteroidEntity.Id, _texture.Height, _texture.Width);
        componentManager.AddDealDamageComponent(asteroidEntity.Id, Damage);
        componentManager.AddTakeDamageComponent(asteroidEntity.Id, Health);
        componentManager.AddAllegianceComponent(asteroidEntity.Id, Allegiance.Enemy);
        componentManager.AddScoreComponent(asteroidEntity.Id, Score);
    }
}