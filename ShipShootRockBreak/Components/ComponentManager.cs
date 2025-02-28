using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShipShootRockBreak.Constants;

namespace ShipShootRockBreak.Components;

public class ComponentManager
{
    public Dictionary<Guid, RenderComponent> RenderComponents { get; } = new();
    public Dictionary<Guid, PositionComponent> PositionComponents { get; } = new();
    public Dictionary<Guid, LinearMotionComponent> MotionComponents { get; } = new();
    public Dictionary<Guid, CollisionComponent> CollisionComponents { get; } = new(); 
    public Dictionary<Guid, DealDamageComponent> DealDamageComponents { get; } = new();
    public Dictionary<Guid, TakeDamageComponent> TakeDamageComponents { get; } = new();
    public Dictionary<Guid, TextRenderComponent> TextComponents { get; } = new();
    public Dictionary<Guid, DeadComponent> DeadComponents { get; } = new();
    public Dictionary<Guid, VisibleComponent> VisibleComponents { get; } = new();
    public Dictionary<Guid, AngularMotionComponent> AngularMotionComponents { get; } = new();
    public Dictionary<Guid, RotationComponent> RotationComponents { get; } = new();
    public Dictionary<Guid, AllegianceComponent> AllegianceComponents { get; } = new();
    public Dictionary<Guid, ScoreComponent> ScoreComponents { get; } = new();
    public Dictionary<Guid, TotalScoreComponent> TotalScoreComponents { get; } = new();
    public Dictionary<Guid, PlayerComponent> PlayerComponents { get; } = new();
    public Dictionary<Guid, GameOverComponent> GameOverComponents { get; } = new();

    #region Add component to entity
    public void AddRenderComponent(Guid id, Texture2D texture)
    {
        RenderComponents.Add(id, new RenderComponent(texture));
    }
    
    public void AddPositionComponent(Guid id, Vector2 position)
    {
        PositionComponents.Add(id, new PositionComponent(position));
    }

    public void AddLinearMotionComponent(Guid id, Vector2 initialVelocity)
    {
        MotionComponents.Add(id, new LinearMotionComponent(initialVelocity));
    }

    public void AddCollisionComponent(Guid id, int height, int width)
    {
        CollisionComponents.Add(id, new CollisionComponent(height, width));
    }

    public void AddDealDamageComponent(Guid id, int damage)
    {
        DealDamageComponents.Add(id, new DealDamageComponent(damage));
    }

    public void AddTakeDamageComponent(Guid id, int startingHealth)
    {
        TakeDamageComponents.Add(id, new TakeDamageComponent(startingHealth));
    }

    public void AddTextRenderComponent(Guid id, SpriteFont font, string baseText)
    {
        TextComponents.Add(id, new TextRenderComponent(font, baseText));
    }

    public void AddDeadComponent(Guid id)
    {
        DeadComponents.Add(id, new DeadComponent());
    }

    public void AddVisibleComponent(Guid id)
    {
        VisibleComponents.Add(id, new VisibleComponent());
    }

    public void AddAngularMotionComponent(Guid id, float angularSpeed)
    {
        AngularMotionComponents.Add(id, new AngularMotionComponent(angularSpeed));
    }

    public void AddRotationComponent(Guid id, float angle = 0)
    {
        RotationComponents.Add(id, new RotationComponent(angle));
    }

    public void AddAllegianceComponent(Guid id, Allegiance allegiance)
    {
        AllegianceComponents.Add(id, new AllegianceComponent(allegiance));
    }

    public void AddScoreComponent(Guid id, int score)
    {
        ScoreComponents.Add(id, new ScoreComponent(score));
    }

    public void AddTotalScoreComponent(Guid id)
    {
        TotalScoreComponents.Add(id, new TotalScoreComponent());
    }

    public void AddPlayerComponent(Guid id)
    {
        PlayerComponents.Add(id, new PlayerComponent());
    }

    public void AddGameOverComponent(Guid id)
    {
        GameOverComponents.Add(id, new GameOverComponent());
    }
    #endregion
    
    // TODO: At the moment we get a component for an entity by directly accessing the component dictionary
    // Should we have getter functions instead?
}