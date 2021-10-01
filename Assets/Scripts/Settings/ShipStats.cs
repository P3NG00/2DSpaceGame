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

        public Color ColorPrimary => colorPrimary;
        public Color ColorSecondary => colorSecondary;
        public float MultiplierForce => multiplierForce;
        public float MultiplierRotate => multiplierRotate;
        public float Drag => drag;
    }
}
