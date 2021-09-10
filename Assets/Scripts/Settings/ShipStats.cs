using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats", fileName = "Ship Stats")]
    public sealed class ShipStats : ScriptableObject
    {
        [SerializeField] private Color colorPrimary; // TODO implement colorPrimary?
        [SerializeField] private Color colorSecondary; // TODO implement colorSecondary?
        [SerializeField, Min(0f)] private float multiplierForce;
        [SerializeField, Min(0f)] private float multiplierRotate;
        [SerializeField, Min(0f)] private float velocityMissile;
        [SerializeField, Min(0f)] private float timeMissileLife;
        [SerializeField, Min(0f)] private float timeBetweenShots;

        // public Color ColorPrimary => colorPrimary;
        // public Color ColorSecondary => colorSecondary;
        public float MultiplierForce => multiplierForce;
        public float MultiplierRotate => multiplierRotate;
        public float VelocityMissile => velocityMissile;
        public float TimeMissileLife => timeMissileLife;
        public float TimeBetweenShots => timeBetweenShots;
    }
}
