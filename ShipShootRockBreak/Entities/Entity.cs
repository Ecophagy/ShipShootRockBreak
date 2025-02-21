using System;

namespace ShipShootRockBreak.Entities;

public class Entity(string entityName)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string EntityName { get; } = entityName; // Human-readable, for debugging
}