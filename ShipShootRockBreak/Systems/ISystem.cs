using Microsoft.Xna.Framework;
using ShipShootRockBreak.Components;

namespace ShipShootRockBreak.Systems;

public interface ISystem
{
   public void Update(GameTime gameTime, ComponentManager componentManager);
}