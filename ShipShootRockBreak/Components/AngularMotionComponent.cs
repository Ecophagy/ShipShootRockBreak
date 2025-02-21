namespace ShipShootRockBreak.Components;

public class AngularMotionComponent(float angularSpeed)
{
    public float AngularVelocity { get; set; } = 0; // Rad per second
    public float AngularSpeed { get; } = angularSpeed; // Rad per second
}