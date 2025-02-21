using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Entities;

public class BulletFactory(Texture2D texture)
{
    private Texture2D _texture = texture;
    private const int Damage = 10;
    private const int Health = 1;

    public void CreateBullet(Dictionary<Guid, RenderComponent> renderComponents,
                            Dictionary<Guid, PositionComponent> positionComponents,
                            Dictionary<Guid, RotationComponent> rotationComponents,
                            Dictionary<Guid, LinearMotionComponent> linearMotionComponents,
                            Dictionary<Guid, CollisionComponent> collisionComponents,
                            Dictionary<Guid, DealDamageComponent> dealDamageComponents,
                            Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
                            Vector2 position,
                            Vector2 velocity,
                            float rotation)
    {
        var bulletEntity = new Entity("bullet");
        renderComponents.Add(bulletEntity.Id, new RenderComponent(_texture));
        positionComponents.Add(bulletEntity.Id, new PositionComponent(position));
        rotationComponents.Add(bulletEntity.Id, new RotationComponent(rotation));
        linearMotionComponents.Add(bulletEntity.Id, new LinearMotionComponent(velocity));
        collisionComponents.Add(bulletEntity.Id, new CollisionComponent(_texture.Height, _texture.Width));
        dealDamageComponents.Add(bulletEntity.Id, new DealDamageComponent(Damage));
        takeDamageComponents.Add(bulletEntity.Id, new TakeDamageComponent(Health));

    }
}