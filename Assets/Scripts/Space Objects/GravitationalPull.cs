using UnityEngine;

namespace SpaceGame.SpaceObjects
{
    public sealed class GravitationalPull : MonoBehaviour
    {
        [Header("References [GravitationalPull]", order = 90)]
        [SerializeField] private float gravityScale = 1f;
        [SerializeField] private SpaceObjectPlanet planet;

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject != this.planet.gameObject)
            {
                // TODO
                // get rigidbody (of anything)
                // add velocity to drag towards planet
                // stronger velocity closer to center

                // Distance is divided by two because planet takes up half of raidus
                Vector2 offset = this.transform.position - collider.transform.position;
                float distance = offset.magnitude;
                float maxDistance = this.planet.Scale;

                if (distance < maxDistance)
                {
                    float scale = 1f - (distance / maxDistance);
                    Vector2 gravity = offset.normalized * scale * gravityScale * Time.deltaTime;
                    collider.attachedRigidbody.AddForce(gravity);
                }
            }
        }
    }
}
