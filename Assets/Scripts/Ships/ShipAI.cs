namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        private void FixedUpdate()
        {
            this.AddForce();
            this.RotateToLookAt(GameInfo.Player.Position);
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
