using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class Planet : SpaceObject
    {
        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            // TODO test missles not passing through planet
            if (collider.tag == "Missile")
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
