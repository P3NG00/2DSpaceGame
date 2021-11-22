using UnityEngine;

namespace SpaceGame.Ships
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats/Ship", fileName = "Ship Info")]
    public class ShipInfo : ScriptableObject
    {
        [Header("Info [ShipStats]", order = 0)]
        [SerializeField] private Color colorPrimary;
        [SerializeField] private Color colorSecondary;
        [SerializeField, Min(0f)] private float multiplierForce;
        [SerializeField, Min(0f)] private float multiplierRotate;
        [SerializeField] private float drag;
        [SerializeField, Min(0f)] private float scaleCollisionDamage;
        [SerializeField, Min(0f)] private float scaleMissileDamage;
        [SerializeField, Min(0f)] private float maxMagnitude;
        [SerializeField] private float boostMagnitude;
        [SerializeField] private float boostSoundTimeBetween;

        public Color ColorPrimary => this.colorPrimary;
        public Color ColorSecondary => this.colorSecondary;
        public float MultiplierForce => this.multiplierForce;
        public float MultiplierRotate => this.multiplierRotate;
        public float Drag => this.drag;
        public float ScaleCollisionDamage => this.scaleCollisionDamage;
        public float ScaleMissileDamage => this.scaleMissileDamage;
        public float MaxMagnitude => this.maxMagnitude;
        public float BoostMagnitude => this.boostMagnitude;
        public float BoostSoundTimeBetween => this.boostSoundTimeBetween;
    }
}
