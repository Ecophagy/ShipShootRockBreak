using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Entities;
using ShipShootRockBreak.Systems;

namespace ShipShootRockBreak;

public class Game1 : Game
{
    // Core
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public int ScreenWidth;
    public int ScreenHeight;

    // Entities
    private readonly Entity _shipEntity = new("ship");
    private readonly Entity _bulletEntity = new("bullet");
    private readonly Entity _asteroid = new("asteroid");
    
    // Components
    private Dictionary<Guid, RenderComponent> _renderComponents = new();
    private Dictionary<Guid, PositionComponent> _positionComponents = new();
    private Dictionary<Guid, MotionComponent> _motionComponents = new();
    private Dictionary<Guid, CollisionComponent> _collisionComponents = new();
    
    // Systems
    private readonly RenderSystem _renderSystem = new();
    private readonly MotionSystem _motionSystem = new();
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ScreenWidth = _graphics.PreferredBackBufferWidth;
        ScreenHeight = _graphics.PreferredBackBufferHeight;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Ship
        var shipTexture = this.Content.Load<Texture2D>("ship");
        _renderComponents.Add(_shipEntity.Id, new RenderComponent(shipTexture));
        _positionComponents.Add(_shipEntity.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (shipTexture.Width / 2), (ScreenHeight / 2) - (shipTexture.Height / 2))));

        // Bullet
        var bulletTexture = this.Content.Load<Texture2D>("bullet");
        _renderComponents.Add(_bulletEntity.Id, new RenderComponent(bulletTexture));
        _positionComponents.Add(_bulletEntity.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (bulletTexture.Width / 2), (ScreenHeight / 2) - (bulletTexture.Height / 2))));
        _motionComponents.Add(_bulletEntity.Id, new MotionComponent(new Vector2(0f, -20f)));

        // Asteroid
        var asteroidTexture = this.Content.Load<Texture2D>("asteroid");
        _renderComponents.Add(_asteroid.Id, new RenderComponent(asteroidTexture));
        _positionComponents.Add(_asteroid.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (asteroidTexture.Width / 2), 0f)));
        _motionComponents.Add(_asteroid.Id, new MotionComponent(new Vector2(0f, 20f)));
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var (entityId, component) in _motionComponents)
        {
            _motionSystem.Update(gameTime, component, _positionComponents[entityId]);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();

        foreach (var (entityId, component) in _renderComponents)
        {
            _renderSystem.Draw(_spriteBatch, component, _positionComponents[entityId]);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}