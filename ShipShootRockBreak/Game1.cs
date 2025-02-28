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
    // TODO: Put these in an ordered list and iterate over it for updates
    private readonly RenderSystem _renderSystem = new();
    private readonly TextRenderSystem _textRenderSystem = new();
    private readonly LinearMotionSystem _linearMotionSystem = new();
    private readonly AngularMotionSystem _angularMotionSystem = new();
    private readonly DamageSystem _damageSystem = new();
    private readonly DeathSystem _deathSystem = new();
    private readonly GameOverSystem _gameOverSystem = new();
    private readonly ShipUserControlSystem _shipUserControlSystem = new();
    private readonly FireBulletSystem _fireBulletSystem = new();
    private readonly AsteroidSpawnSystem _asteroidSpawnSystem = new();
    private readonly ScoreSystem _scoreSystem = new();
    private readonly ScoreboardUpdateSystem _scoreboardUpdateSystem = new();
    
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
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _shipUserControlSystem.Update(gameTime, _componentManager);
        _fireBulletSystem.Update(gameTime,
                                _componentManager,
                                _bulletFactory);
        _asteroidSpawnSystem.Update(gameTime, _componentManager, _asteroidFactory);

        _linearMotionSystem.Update(gameTime, _componentManager);
        _angularMotionSystem.Update(gameTime, _componentManager);

        _damageSystem.Update(gameTime, _componentManager);
        
        _scoreSystem.Update(gameTime, _componentManager);
        _scoreboardUpdateSystem.Update(gameTime, _componentManager);
        _gameOverSystem.Update(gameTime, _componentManager, _shipEntity, _gameOver);
        
        PostUpdate(gameTime);

        base.Update(gameTime);
    }

    private void PostUpdate(GameTime gameTime)
    {
        _deathSystem.Update(gameTime, _componentManager);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        
        _renderSystem.Draw(_spriteBatch, _componentManager);
        _textRenderSystem.Draw(_spriteBatch, _componentManager);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}