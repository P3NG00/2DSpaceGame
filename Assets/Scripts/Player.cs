using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
{
    public sealed class Player : MonoBehaviour
    {
        [Header("References", order = 99)]
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private Rigidbody2D prefabMissile;
        [SerializeField] private SpriteRenderer srPrimary;
        [SerializeField] private SpriteRenderer srSecondary;
        [SerializeField] private new ParticleSystem particleSystem;
        [SerializeField] private Animator animator;

        public Rigidbody2D Rigidbody => rigidbody;

        private bool inputAddForce = false;
        private bool inputFire = false;
        private float inputRotation = 0f;

        private ShipStats stats;

        private void Update()
        {
            UpdateForce();
            UpdateRotation();
            particleSystem.transform.position = transform.position;
            animator.SetBool("Moving", inputAddForce);
        }

        private void Start()
        {
            stats = GameInfo.GMSettings.ShipStatsPlayer;
        }

        private void UpdateForce()
        {
            if (inputAddForce)
            {
                Vector2 direction = transform.up;
                Vector2 velocity = direction * stats.MultiplierForce;
                rigidbody.AddForce(velocity);
            }
        }

        private void UpdateRotation()
        {
            if (inputRotation != 0f)
            {
                Vector2 force = transform.right * inputRotation;
                force *= stats.MultiplierRotate * Time.deltaTime;

                Vector2 position = transform.position;
                position += (Vector2)transform.up * 0.5773f;

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

                missile.velocity = transform.up * stats.VelocityMissile;

                Destroy(missile.gameObject, stats.TimeMissileLife);

                yield return new WaitForSeconds(stats.TimeBetweenShots);
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
        public void Exit(InputAction.CallbackContext ctx) => Application.Quit();
    }
}
