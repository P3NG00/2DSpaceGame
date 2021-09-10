using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Ships
{
    public sealed class ShipPlayer : Ship
    {
        [Header("References (as ShipPlayer)", order = 99)]
        [SerializeField] private Transform parentParticleSystems;
        [SerializeField] private Animator animator;

        private bool inputAddForce = false;
        private bool inputFire = false;
        private float inputRotation = 0f;

        private void Update()
        {
            UpdateForce();
            UpdateRotation();
            parentParticleSystems.position = transform.position;
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
                AddForce();
            }
        }

        private void UpdateRotation()
        {
            if (inputRotation != 0f)
            {
                Rotate(inputRotation);
            }
        }

        private IEnumerator RoutineFire()
        {
            while (inputFire)
            {
                Fire();

                yield return new WaitForSeconds(stats.TimeBetweenShots);
            }
        }

        public void InputAddForce(InputAction.CallbackContext ctx) => inputAddForce = ctx.performed;
        public void InputFire(InputAction.CallbackContext ctx)
        {
            inputFire = ctx.performed;

            if (inputFire)
            {
                StartCoroutine(RoutineFire());
            }
        }
        public void InputRotate(InputAction.CallbackContext ctx) => inputRotation = ctx.ReadValue<float>();
        public void InputExit(InputAction.CallbackContext ctx) => Application.Quit();
    }
}
