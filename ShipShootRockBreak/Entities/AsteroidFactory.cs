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

    public void CreateAsteroid(Dictionary<Guid, RenderComponent> renderComponents,
        Dictionary<Guid, PositionComponent> positionComponents,
        Dictionary<Guid, RotationComponent> rotationComponents,
        Dictionary<Guid, LinearMotionComponent> linearMotionComponents,
        Dictionary<Guid, CollisionComponent> collisionComponents,
        Dictionary<Guid, DealDamageComponent> dealDamageComponents,
        Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
        Dictionary<Guid, AllegianceComponent> allegianceComponents,
        Vector2 position,
        Vector2 velocity)
    {
        var asteroidEntity = new Entity("asteroid");
        renderComponents.Add(asteroidEntity.Id, new RenderComponent(_texture));
        positionComponents.Add(asteroidEntity.Id, new PositionComponent(position));
        rotationComponents.Add(asteroidEntity.Id, new RotationComponent());
        linearMotionComponents.Add(asteroidEntity.Id, new LinearMotionComponent(velocity));
        collisionComponents.Add(asteroidEntity.Id, new CollisionComponent(_texture.Height, _texture.Width));
        dealDamageComponents.Add(asteroidEntity.Id, new DealDamageComponent(Damage));
        takeDamageComponents.Add(asteroidEntity.Id, new TakeDamageComponent(Health));
        allegianceComponents.Add(asteroidEntity.Id, new AllegianceComponent(Allegiance.Enemy));
    }
}