using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class SpaceObject : MonoBehaviour
    {
        [Header("Info [SpaceObject]", order = 5)]
        public SpaceObjectInfo Settings;

        [Header("References [SpaceObject]", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        // Private Cache
        private bool alive = true;

        // Public Getters
        public SpriteRenderer SpriteRenderer => this.spriteRenderer;
        public Rigidbody2D Rigidbody => this.rigidbody;

        // Public Properties
        public float Scale => this.transform.localScale.x;

        public void Damage(float amount, Enums.DamageType damageType)
        {
            if (this.alive)
            {
                float scale = this.Scale;

                switch (damageType)
                {
                    case Enums.DamageType.Collision: scale -= amount * this.Settings.DamageScaleCollision; break;
                    case Enums.DamageType.Weapon: scale -= amount * this.Settings.DamageScaleProjectile; break;
                }

                if (scale < this.Settings.DestroyBelowScale)
                {
                    this.alive = false;
                    GameInfo.DestroySpaceObject(this);
                    SpaceObjectItem itemObject = (SpaceObjectItem)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, this.transform.position);
                    itemObject.SetItem(this.Settings.RandomItemDrop);
                }
                else
                {
                    this.transform.localScale = Vector2.one * scale;
                }
            }
        }

        // Unity Start Method
        private void Start() => this.SpriteRenderer.color = this.GetColor();

        protected virtual Color GetColor() => this.Settings.Color;
    }
}
