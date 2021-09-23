using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class Planet : SpaceObject
    {
        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);
        }
    }
}
