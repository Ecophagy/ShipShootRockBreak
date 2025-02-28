using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class FireBulletSystem(BulletFactory bulletFactory) : ISystem
{
    private float Timer { get; set; }
    private readonly BulletFactory _bulletFactory = bulletFactory;
    
    public void Update(GameTime gameTime,
                    ComponentManager componentManager)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            if (Timer >= GameConstants.BulletCreationThrottle)
            {
                foreach(var (entityId, component) in componentManager.PlayerComponents)
                {
                    var shipLocation = componentManager.PositionComponents[entityId];
                    var shipRotation = componentManager.RotationComponents[entityId];
                    var horizontalOffset = Math.Sin(shipRotation.Rotation) * GameConstants.SpawnDistanceFromShip;
                    var verticalOffset = Math.Cos(shipRotation.Rotation) * GameConstants.SpawnDistanceFromShip;

                    var bulletPosition = shipLocation.Position +
                                         new Vector2((float)horizontalOffset, -(float)verticalOffset);
                    
                    var bulletVelocity =
                        new Vector2((float)(Math.Sin(shipRotation.Rotation) * GameConstants.BulletSpeed),
                            -(float)(Math.Cos(shipRotation.Rotation) * GameConstants.BulletSpeed));

                    _bulletFactory.CreateBullet(componentManager,
                        bulletPosition,
                        bulletVelocity,
                        shipRotation.Rotation);
                    Timer = 0;
                }
            }
        }
    }
}