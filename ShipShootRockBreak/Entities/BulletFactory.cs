using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;

namespace ShipShootRockBreak.Entities;

public class BulletFactory(Texture2D texture)
{
    private readonly Texture2D _texture = texture;
    private const int Damage = 10;
    private const int Health = 1;

    public void CreateBullet(ComponentManager componentManager,
                            Vector2 position,
                            Vector2 velocity,
                            float rotation)
    {
        var bulletEntity = new Entity("bullet");
        componentManager.AddRenderComponent(bulletEntity.Id, _texture);
        componentManager.AddPositionComponent(bulletEntity.Id, position);
        componentManager.AddRotationComponent(bulletEntity.Id, rotation);
        componentManager.AddLinearMotionComponent(bulletEntity.Id, velocity);
        componentManager.AddCollisionComponent(bulletEntity.Id, _texture.Height, _texture.Width);
        componentManager.AddDealDamageComponent(bulletEntity.Id, Damage);
        componentManager.AddTakeDamageComponent(bulletEntity.Id, Health);
        componentManager.AddAllegianceComponent(bulletEntity.Id, Allegiance.Ally);
    }
}