using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class FireBulletSystem(float creationThrottle)
{
    private float CreationThrottle { get; } = creationThrottle;
    private float Timer { get; set; }

    private const int DistanceFromShip = 16;
    private const int BulletSpeed = 50;
    
    public void Update(GameTime gameTime,
                    BulletFactory bulletFactory,
                    Guid shipId,
                    Dictionary<Guid, RenderComponent> renderComponents,
                    Dictionary<Guid, PositionComponent> positionComponents,
                    Dictionary<Guid, RotationComponent> rotationComponents,
                    Dictionary<Guid, LinearMotionComponent> linearMotionComponents,
                    Dictionary<Guid, CollisionComponent> collisionComponents,
                    Dictionary<Guid, DealDamageComponent> dealDamageComponents,
                    Dictionary<Guid, TakeDamageComponent> takeDamageComponents,
                    Dictionary<Guid, AllegianceComponent> allegianceComponents)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            if (Timer >= CreationThrottle)
            {
                var shipLocation = positionComponents[shipId];
                var shipRotation = rotationComponents[shipId];
                var horizontalOffset = Math.Sin(shipRotation.Rotation) * DistanceFromShip;
                var verticalOffset = Math.Cos(shipRotation.Rotation) * DistanceFromShip;
                
                var bulletPosition = shipLocation.Position + new Vector2((float)horizontalOffset, -(float)verticalOffset); ;
                var bulletVelocity = new Vector2((float)(Math.Sin(shipRotation.Rotation) * BulletSpeed), -(float)(Math.Cos(shipRotation.Rotation) * BulletSpeed));
                
                bulletFactory.CreateBullet(renderComponents,
                    positionComponents,
                    rotationComponents,
                    linearMotionComponents,
                    collisionComponents,
                    dealDamageComponents,
                    takeDamageComponents,
                    allegianceComponents,
                    bulletPosition,
                    bulletVelocity,
                    shipRotation.Rotation);
                Timer = 0;
            }
        }
    }
}