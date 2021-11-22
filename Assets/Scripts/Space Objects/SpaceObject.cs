using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public class SpaceObject : MonoBehaviour
    {
        [Header("Info [SpaceObject]", order = 5)]
        public SpaceObjectInfo SpaceObjectInfo;

        [Header("References [SpaceObject]", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private AudioSource audioSource;

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
                    case Enums.DamageType.Collision: scale -= amount * this.SpaceObjectInfo.DamageScaleCollision; break;
                    case Enums.DamageType.Weapon: scale -= amount * this.SpaceObjectInfo.DamageScaleProjectile; break;
                }

                if (scale < this.SpaceObjectInfo.DestroyBelowScale)
                {
                    this.alive = false;
                    GameInfo.DestroySpaceObject(this);
                    SpaceObjectItem itemObject = (SpaceObjectItem)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, this.transform.position);
                    itemObject.SetItem(this.SpaceObjectInfo.RandomItemDrop);
                }
                else
                {
                    this.transform.localScale = Vector2.one * scale;
                }
            }
        }

        // Unity Start Method
        private void Start() => this.SpriteRenderer.color = this.GetColor();

        protected virtual Color GetColor() => this.SpaceObjectInfo.Color;

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.alive && collision.relativeVelocity.magnitude > 10f) // TODO this "10f" can be changed if sounds arent happening at low velocity
            {
                Vector2 cameraPoint = Camera.main.WorldToViewportPoint(collision.contacts[0].point);

                // If point within camera view...
                if (cameraPoint.x > 0f && cameraPoint.x < 1f && cameraPoint.y > 0f && cameraPoint.y < 1f)
                {
                    // Play collision sound
                    GameInfo.SoundManager.PlayCollision(this.audioSource);
                }
            }
        }
    }
}
