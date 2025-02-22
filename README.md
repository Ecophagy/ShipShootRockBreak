# Asteroids

This game is a recreation of arcade classic Astreroids, built in the entity-component-system paradigm as a learning exercise.

## Archticture
- Entity: An ID (GUID) that connects different components together, which may be created by a factory (e.g. ship, asteroid)
- Component: A data-only container that indicates capability of an entity (e.g. position, velocity, health)
- System: Stateless "Updates" that act on combinations of components to modify them (e.g. position + velocity components to a new position)
