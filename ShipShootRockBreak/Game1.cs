using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShipShootRockBreak.Components;
using ShipShootRockBreak.Constants;
using ShipShootRockBreak.Entities;
using ShipShootRockBreak.Systems;

namespace ShipShootRockBreak;

public class Game1 : Game
{
    // Core
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static int ScreenWidth;
    public static int ScreenHeight;

    // Entities
    private readonly Entity _shipEntity = new("ship");
    private BulletFactory _bulletFactory;
    private AsteroidFactory _asteroidFactory;
    private readonly Entity _asteroid = new("asteroid");
    private readonly Entity _gameOver = new("game_over");
    
    // Components
    private Dictionary<Guid, RenderComponent> _renderComponents = new();
    private Dictionary<Guid, PositionComponent> _positionComponents = new();
    private Dictionary<Guid, LinearMotionComponent> _motionComponents = new();
    private Dictionary<Guid, CollisionComponent> _collisionComponents = new();
    private Dictionary<Guid, DealDamageComponent> _dealDamageComponents = new();
    private Dictionary<Guid, TakeDamageComponent> _takeDamageComponents = new();
    private Dictionary<Guid, TextRenderComponent> _textComponents = new();
    private Dictionary<Guid, DeadComponent> _deadComponents = new();
    private Dictionary<Guid, VisibleComponent> _visibleComponents = new();
    private Dictionary<Guid, AngularMotionComponent> _angularMotionComponents = new();
    private Dictionary<Guid, RotationComponent> _rotationComponents = new();
    private Dictionary<Guid, AllegianceComponent> _allegianceComponents = new();
    
    // Systems
    private readonly RenderSystem _renderSystem = new();
    private readonly TextRenderSystem _textRenderSystem = new();
    private readonly LinearMotionSystem _linearMotionSystem = new();
    private readonly AngularMotionSystem _angularMotionSystem = new();
    private readonly DamageSystem _damageSystem = new();
    private readonly DeathSystem _deathSystem = new();
    private readonly GameOverSystem _gameOverSystem = new();
    private readonly ShipUserControlSystem _shipUserControlSystem = new();
    private readonly FireBulletSystem _fireBulletSystem = new(0.25f);
    private readonly AsteroidSpawnSystem _asteroidSpawnSystem = new(1);
    
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
        var shipTexture = this.Content.Load<Texture2D>("rocket");
        _renderComponents.Add(_shipEntity.Id, new RenderComponent(shipTexture));
        _positionComponents.Add(_shipEntity.Id, new PositionComponent(new Vector2((ScreenWidth / 2) - (shipTexture.Width / 2), (ScreenHeight / 2) - (shipTexture.Height / 2))));
        _rotationComponents.Add(_shipEntity.Id, new RotationComponent());
        _angularMotionComponents.Add(_shipEntity.Id, new AngularMotionComponent(MathHelper.ToRadians(135)));
        _collisionComponents.Add(_shipEntity.Id, new CollisionComponent(shipTexture.Height, shipTexture.Width));
        _takeDamageComponents.Add(_shipEntity.Id, new TakeDamageComponent(100));
        _allegianceComponents.Add(_shipEntity.Id, new AllegianceComponent(Allegiance.Ally));
        
        // Bullet
        var bulletTexture = this.Content.Load<Texture2D>("bullet");
        _bulletFactory = new BulletFactory(bulletTexture);
      
        // Asteroid
        var asteroidTexture = this.Content.Load<Texture2D>("asteroid");
        _asteroidFactory = new AsteroidFactory(asteroidTexture);
        
        // Game Over
        var spriteFont = Content.Load<SpriteFont>("HudFont");
        _textComponents.Add(_gameOver.Id, new TextRenderComponent(spriteFont, "Game Over!"));
        _positionComponents.Add(_gameOver.Id, new PositionComponent(new Vector2(ScreenWidth/2, ScreenHeight/2) - _textComponents[_gameOver.Id].Size / 2)); 
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _shipUserControlSystem.Update(_angularMotionComponents[_shipEntity.Id]);
        _fireBulletSystem.Update(gameTime, 
                                _bulletFactory,
                                _shipEntity.Id,
                                _renderComponents,
                                _positionComponents,
                                _rotationComponents,
                                _motionComponents,
                                _collisionComponents,
                                _dealDamageComponents,
                                _takeDamageComponents,
                                _allegianceComponents);
        _asteroidSpawnSystem.Update(gameTime,
                                    _asteroidFactory,
                                    _shipEntity.Id,
                                    _renderComponents,
                                    _positionComponents,
                                    _rotationComponents,
                                    _motionComponents,
                                    _collisionComponents,
                                    _dealDamageComponents,
                                    _takeDamageComponents,
                                    _allegianceComponents);

        _linearMotionSystem.Update(gameTime, _motionComponents, _positionComponents);
        _angularMotionSystem.Update(gameTime, _angularMotionComponents, _rotationComponents);

        _damageSystem.Update(_collisionComponents,
                            _positionComponents,
                            _dealDamageComponents,
                            _takeDamageComponents,
                            _deadComponents,
                            _allegianceComponents);
        
        _gameOverSystem.Update(_shipEntity, _gameOver, _deadComponents, _visibleComponents);
        
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
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // TODO: Update these to take dictionaries of components?
        foreach (var (entityId, component) in _renderComponents)
        {
            _renderSystem.Draw(_spriteBatch, component, _positionComponents[entityId], _rotationComponents[entityId]);
        }
        
        foreach (var (entityId, component) in _textComponents)
        {
            if (_visibleComponents.ContainsKey(entityId))
            {
                _textRenderSystem.Draw(_spriteBatch, component, _positionComponents[entityId]);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}