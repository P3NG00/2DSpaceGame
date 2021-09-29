namespace SpaceGame.Ships
{
    public sealed class ShipAI : Ship
    {
        // TODO
        private void Update()
        {
        }

        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
