namespace SpaceGame
{
    public sealed class Enums
    {
        public enum DamageType
        {
            Collision,
            Projectile,
        }

        public enum Direction
        {
            Down,
            Left,
            Right,
            Up,
        }

        public enum RotationType
        {
            RotateAxis,     // Rotate Left/Right keys (controller & keyboard)
            AimAtMouse,     // Mouse pointer
            AimInDirection, // Controllers (right stick on controller)
        }

        public enum ShipAIType
        {
            Passive,
            Aggressive,
            Stalk,
        }

        public enum SpaceObjectSpawnAreaType
        {
            Default,
            AroundPlayer,
            FrontOfPlayer,
        }

        public enum SpaceObjectSpawnRateType
        {
            Default,
            ScaleWithMagnitude,
            SingleInstance,
        }
    }
}