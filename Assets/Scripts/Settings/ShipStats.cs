using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats", fileName = "Ship Stats")]
    public sealed class ShipStats : ScriptableObject
    {
        [Header("Info", order = 10)]
        [SerializeField] private Color colorPrimary;
        [SerializeField] private Color colorSecondary;
        [SerializeField, Min(0f)] private float multiplierForce;
        [SerializeField, Min(0f)] private float multiplierRotate;
        [SerializeField] private float drag;

        public Color ColorPrimary => this.colorPrimary;
        public Color ColorSecondary => this.colorSecondary;
        public float MultiplierForce => this.multiplierForce;
        public float MultiplierRotate => this.multiplierRotate;
        public float Drag => this.drag;
    }
}
