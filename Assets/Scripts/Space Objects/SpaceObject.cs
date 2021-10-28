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

        public SpriteRenderer SpriteRenderer => this.spriteRenderer;
        public Rigidbody2D Rigidbody => this.rigidbody;

        private bool alive = true;

        public float Scale
        {
            get => this.transform.localScale.x;
            set
            {
                if (this.alive)
                {
                    if (value < this.Settings.DestroyBelowScale)
                    {
                        this.alive = false;
                        GameInfo.DestroySpaceObject(this);
                        SpaceObjectItem itemObject = (SpaceObjectItem)GameInfo.SpawnSpaceObject(GameInfo.SettingsItemObject, this.transform.position);
                        itemObject.SetItem(this.Settings.RandomItemDrop);
                    }
                    else
                    {
                        this.transform.localScale = Vector2.one * value;
                    }
                }
            }
        }

        private void Start()
        {
            this.SpriteRenderer.color = this.GetColor();
        }

        protected virtual Color GetColor() => this.Settings.Color;
    }
}
