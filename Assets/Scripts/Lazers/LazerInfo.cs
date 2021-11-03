using SpaceGame.Items;
using UnityEngine;

namespace SpaceGame.Lazers
{
    [CreateAssetMenu(menuName = "2D Space Game/Lazer/Info", fileName = "Lazer Info")]
    public sealed class LazerInfo : ScriptableObject
    {
        [Header("Info [LazerInfo]", order = 0)]
        [SerializeField] private float damage;
        [SerializeField] private float length;

        [Header("References [LazerInfo]", order = 105)]
        [SerializeField] private ItemInfoLazer correspondingItem;
        [SerializeField] private Lazer lazerObject;

        public float Damage => this.damage;
        public float Length => this.length;
        public ItemInfoLazer Item => this.correspondingItem;
        public Lazer LazerObject => this.lazerObject;
    }
}
