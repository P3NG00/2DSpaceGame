using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
    public sealed class Player : MonoBehaviour
    {
        [Header("Stats", order = 5)]
        [SerializeField, Min(0f)] private float multForce = 1f;
        [SerializeField, Min(0f)] private float multRotate = 1f;
        [SerializeField, Min(0f)] private float velocityMissile = 1f;
        [SerializeField, Min(0f)] private float timeMissileLife = 1f;
        [SerializeField, Min(0f)] private float timeBetweenShots = 1f;

        [Header("Other", order = 50)]
        [SerializeField] private Color colorShip;
        [SerializeField] private Color colorShipTip;

        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private Rigidbody2D prefabMissile;
        [SerializeField] private SpriteRenderer srShip;
        [SerializeField] private SpriteRenderer srShipTip;
        [SerializeField] private new ParticleSystem particleSystem;

        public Rigidbody2D Rigidbody => rigidbody;

        private bool inputAddForce = false;
        private bool inputFire = false;
        private float inputRotation = 0f;

        private void OnValidate()
        {
            srShip.color = colorShip;
            srShipTip.color = colorShipTip;
        }

        private void Update()
        {
            UpdateForce();
            UpdateRotation();
            particleSystem.transform.position = transform.position;
        }

        private void UpdateForce()
        {
            if (inputAddForce)
            {
                Vector2 direction = transform.up;
                Vector2 velocity = direction * multForce;
                rigidbody.AddForce(velocity);
            }
        }

        private void UpdateRotation()
        {
            if (inputRotation != 0f)
            {
                Vector2 force = transform.right * inputRotation;
                force *= multRotate * Time.deltaTime;

                Vector2 position = transform.position;
                position += (Vector2)transform.up * 0.5f;

                rigidbody.AddForceAtPosition(force, position);
            }
        }

        private IEnumerator RoutineFire()
        {
            while (inputFire)
            {
                Vector3 posMissile = transform.position;
                posMissile += transform.up * 0.1f;

                Rigidbody2D missile = Instantiate(prefabMissile, posMissile, transform.rotation);

                missile.velocity = transform.up * velocityMissile;

                Destroy(missile.gameObject, timeMissileLife);

                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        public void AddForce(InputAction.CallbackContext ctx) => inputAddForce = ctx.performed;
        public void Fire(InputAction.CallbackContext ctx)
        {
            inputFire = ctx.performed;

            if (inputFire)
            {
                StartCoroutine(RoutineFire());
            }
        }
        public void Rotate(InputAction.CallbackContext ctx) => inputRotation = ctx.ReadValue<float>();
    }
}
