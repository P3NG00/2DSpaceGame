using UnityEngine;

namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        // TODO needs testing
        private void FixedUpdate()
        {
            this.AddForce();

            Vector2 pos = this.transform.position;
            Vector2 posPlayer = GameInfo.Player.transform.position;
            Vector2 playerOffset = posPlayer - pos;

            // Rotate ship
            float direction = Vector2.Dot(playerOffset.normalized, this.transform.right);
            this.Rotate(direction);
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
