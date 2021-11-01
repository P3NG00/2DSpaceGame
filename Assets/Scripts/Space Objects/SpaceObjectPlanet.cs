using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class SpaceObjectPlanet : SpaceObject
    {
        [Header("References [SpaceObjectPlanet]", order = 105)]
        [SerializeField] private float gravityScale = 1f;

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject != this.gameObject)
            {
                // TODO
                // get rigidbody (of anything)
                // add velocity to drag towards planet
                // stronger velocity closer to center

                // Distance is divided by two because planet takes up half of raidus
                Vector2 offset = this.transform.position - collider.transform.position;
                float distance = offset.magnitude;

                if (distance < this.Scale)
                {
                    float scale = 1f - (distance / this.Scale);
                    Vector2 gravity = offset.normalized * scale * gravityScale * Time.deltaTime;
                    collider.attachedRigidbody.AddForce(gravity);
                }
            }
        }
    }
}
