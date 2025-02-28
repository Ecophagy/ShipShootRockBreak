using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;
using ShipShootRockBreak.Entities;

namespace ShipShootRockBreak.Systems;

public class FireBulletSystem : ISystem
{
    private float Timer { get; set; }
    
    // FIXME: This needs special inputs....
    public void Update(GameTime gameTime,
                    ComponentManager componentManager,
                    BulletFactory bulletFactory)
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

                    bulletFactory.CreateBullet(componentManager,
                        bulletPosition,
                        bulletVelocity,
                        shipRotation.Rotation);
                    Timer = 0;
                }
            }
        }
    }

    public void Update(GameTime gameTime, ComponentManager componentManager)
    {
        throw new NotImplementedException();
    }
}