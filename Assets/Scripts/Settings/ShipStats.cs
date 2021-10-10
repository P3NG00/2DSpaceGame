using UnityEngine;

namespace SpaceGame.Settings
{
    [CreateAssetMenu(menuName = "2D Space Game/Ship Stats/Ship", fileName = "Ship Stats")]
    public class ShipStats : ScriptableObject
    {
        [Header("Info [ShipStats]", order = 0)]
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
