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
    private readonly Entity _scoreboard = new ("scoreboard");
    private readonly Entity _gameOver = new("game_over");
    
    // Components
    private readonly ComponentManager _componentManager = new();
    
    // Systems
    private List<ISystem> _systems;
    private List<IRenderSystem> _renderSystems;
    
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
        _componentManager.AddRenderComponent(_shipEntity.Id, shipTexture);
        _componentManager.AddPositionComponent(_shipEntity.Id, new Vector2((ScreenWidth / 2) - (shipTexture.Width / 2), (ScreenHeight / 2) - (shipTexture.Height / 2)));
        _componentManager.AddRotationComponent(_shipEntity.Id);
        _componentManager.AddAngularMotionComponent(_shipEntity.Id, GameConstants.ShipRotationSpeed);
        _componentManager.AddCollisionComponent(_shipEntity.Id, shipTexture.Height, shipTexture.Width);
        _componentManager.AddTakeDamageComponent(_shipEntity.Id, GameConstants.ShipMaxHealth);
        _componentManager.AddAllegianceComponent(_shipEntity.Id, Allegiance.Ally);
        _componentManager.AddPlayerComponent(_shipEntity.Id);
        
        // Bullet
        var bulletTexture = this.Content.Load<Texture2D>("bullet");
        _bulletFactory = new BulletFactory(bulletTexture);
      
        // Asteroid
        var asteroidTexture = this.Content.Load<Texture2D>("asteroid");
        _asteroidFactory = new AsteroidFactory(asteroidTexture);
     
        var hudFont = Content.Load<SpriteFont>("HudFont");
        
        // Scoreboard
        _componentManager.AddTotalScoreComponent(_scoreboard.Id);
        _componentManager.AddTextRenderComponent(_scoreboard.Id, hudFont, "Score:");
        _componentManager.AddVisibleComponent(_scoreboard.Id);
        _componentManager.AddPositionComponent(_scoreboard.Id, GameConstants.ScoreboardPosition);
        
        // Game Over
        _componentManager.AddTextRenderComponent(_gameOver.Id, hudFont, "Game Over!");
        _componentManager.AddPositionComponent(_gameOver.Id, new Vector2(ScreenWidth/2, ScreenHeight/2) - _componentManager.TextComponents[_gameOver.Id].Size / 2);
        _componentManager.AddGameOverComponent(_gameOver.Id);
        
        // Initialise systems IN THE ORDER you want them to run
        _renderSystems = [new RenderSystem(), new TextRenderSystem()];
        _systems =
        [
            new ShipUserControlSystem(),
            new FireBulletSystem(_bulletFactory),
            new AsteroidSpawnSystem(_asteroidFactory),
            new LinearMotionSystem(),
            new AngularMotionSystem(),
            new DamageSystem(),
            new ScoreSystem(),
            new ScoreboardUpdateSystem(),
            new GameOverSystem(),
            new DeathSystem()
        ];
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var system in _systems)
        {
            system.Update(gameTime, _componentManager);
        }

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        foreach (var system in _renderSystems)
        {
            system.Draw(_spriteBatch, _componentManager);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}