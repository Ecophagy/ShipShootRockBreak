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
    private Dictionary<Guid, DealDamageComponent> _dealDamageComponents = new();
    private Dictionary<Guid, TakeDamageComponent> _takeDamageComponents = new();
    private Dictionary<Guid, DeadComponent> _deadComponents = new();
    
    // Systems
    private readonly RenderSystem _renderSystem = new();
    private readonly MotionSystem _motionSystem = new();
    private readonly DamageSystem _damageSystem = new();
    private readonly DeathSystem _deathSystem = new();
    
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
        _collisionComponents.Add(_shipEntity.Id, new CollisionComponent(shipTexture.Height, shipTexture.Width));
        _takeDamageComponents.Add(_shipEntity.Id, new TakeDamageComponent(100));
        
        // Bullet
        var bulletTexture = this.Content.Load<Texture2D>("bullet");
        _renderComponents.Add(_bulletEntity.Id, new RenderComponent(bulletTexture));
        _positionComponents.Add(_bulletEntity.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (bulletTexture.Width / 2), (ScreenHeight / 2) - (bulletTexture.Height / 2) - shipTexture.Height)));
        _motionComponents.Add(_bulletEntity.Id, new MotionComponent(new Vector2(0f, -20f)));
        _collisionComponents.Add(_bulletEntity.Id, new CollisionComponent(bulletTexture.Height, bulletTexture.Width));
        _dealDamageComponents.Add(_bulletEntity.Id, new DealDamageComponent(10));
        _takeDamageComponents.Add(_bulletEntity.Id, new TakeDamageComponent(1));

        // Asteroid
        var asteroidTexture = this.Content.Load<Texture2D>("asteroid");
        _renderComponents.Add(_asteroid.Id, new RenderComponent(asteroidTexture));
        _positionComponents.Add(_asteroid.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (asteroidTexture.Width / 2), 0f)));
        _motionComponents.Add(_asteroid.Id, new MotionComponent(new Vector2(0f, 20f)));
        _collisionComponents.Add(_asteroid.Id, new CollisionComponent(asteroidTexture.Height, asteroidTexture.Width));
        _dealDamageComponents.Add(_asteroid.Id, new DealDamageComponent(100));
        _takeDamageComponents.Add(_asteroid.Id, new TakeDamageComponent(10));
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _motionSystem.Update(gameTime, _motionComponents, _positionComponents);

        _damageSystem.Update(_collisionComponents,
                            _positionComponents,
                            _dealDamageComponents,
                            _takeDamageComponents,
                            _deadComponents);

        PostUpdate();

        base.Update(gameTime);
    }

    private void PostUpdate()
    {
        // FIXME: Would be preferable to not have to explicitly specify every component list here
        foreach (var (entityId, component) in _deadComponents)
        {
            _deathSystem.Update(entityId,
                _renderComponents,
                _positionComponents,
                _motionComponents,
                _collisionComponents,
                _dealDamageComponents,
                _takeDamageComponents,
                _deadComponents);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();

        // TODO: Update these to take dictionaries of components?
        foreach (var (entityId, component) in _renderComponents)
        {
            _renderSystem.Draw(_spriteBatch, component, _positionComponents[entityId]);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}