# Asteroids

This game is a recreation of arcade classic Astreroids, built in the entity-component-system paradigm as a learning exercise.

## Architecture
- Entity: An ID (GUID) that connects different components together, which may be created by a factory (e.g. ship, asteroid)
- Component: A data-only container that indicates capability of an entity (e.g. position, velocity, health)
- System: Stateless "Updates" that act on combinations of components to modify them (e.g. position + velocity components to a new position)


## Resources
There is a lot of content about ECS, but a lot of it focuses on concepts like reducing CPU cache misses or basic concept without a lot of implementation details. Some good talks I found include:
- https://www.youtube.com/watch?v=SFKR5rZBu-8
- https://gameprogrammingpatterns.com/data-locality.html