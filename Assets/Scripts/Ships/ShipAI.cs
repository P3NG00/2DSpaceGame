namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        // TODO needs testing
        private void FixedUpdate()
        {
            this.AddForce();
            this.Rotate(GetRotationToLookAt(GameInfo.Player.Position));
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
