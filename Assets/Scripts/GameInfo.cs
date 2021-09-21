using System.Collections;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using SpaceGame.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SpaceGame
{
    public sealed class GameInfo : MonoBehaviour
    {
        #region Singleton Instance
        private static GameInfo instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                throw new System.Exception("GameInfo attempted second instance.");
            }
        }
        #endregion

        [Header("Game", order = 0)]
        [SerializeField] private int credits;
        [SerializeField] private GameModeSettings settings;
        [SerializeField] private bool pointTowardsMouse;

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("References", order = 99)]
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceObjects;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text prefabTextCreditPopup;
        [SerializeField] private SpaceObjectSettings settingsItemObject;
        [SerializeField] private Animator uiInvAnimator;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private UIInventorySlot[] inventory = new UIInventorySlot[5];

        private SpaceObject[] SpaceObjects => parentSpaceObjects.GetComponentsInChildren<SpaceObject>();

        private bool inputAddForce = false;
        private bool inputFire = false;
        private bool inputInventory = false;
        private bool inputMenu = false;
        private float inputRotation = 0f;
        private Vector2 inputMousePosition = Vector2.zero;
        private Coroutine routineFiring = null;

        public static GameModeSettings GMSettings => instance.settings;
        public static SpaceObjectSettings SettingsItemObject => instance.settingsItemObject;
        public static Sprite[] Sprites => instance.sprites;

        // Input variables
        public static bool InputAddForce => instance.inputAddForce;
        public static bool InputFire => instance.inputFire;
        public static bool InputInventory => instance.inputInventory;
        public static bool InputMenu => instance.inputMenu;
        public static float InputRotation => instance.inputRotation;

        // Unity Start method
        private void Start()
        {
            foreach (SpaceObjectSettings sos in settings.SpaceObjectsToSpawn)
            {
                StartCoroutine(RoutineSpawnSpaceObject(sos));
            }

            StartCoroutine(RoutineCleanDistantSpaceObjects());
            UpdateTextCredits();
            UpdateInventoryUI();
            Application.targetFrameRate = framerate;
        }

        // Unity Update method
        private void Update()
        {
            if (inputAddForce)
            {
                player.AddForce();
            }

            if (pointTowardsMouse)
            {
                Vector2 playerPosition = player.transform.position;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputMousePosition);

                Vector2 mouseRay = mousePosition - playerPosition;
                Vector2 playerRay = player.transform.right;

                // draw rays
                float rayScale = mouseRay.magnitude;
                mouseRay.Normalize();

                Debug.DrawLine(playerPosition, playerPosition + (Vector2)(player.transform.up * rayScale), Color.magenta);
                Debug.DrawLine(playerPosition, mousePosition, Color.white);

                float direction = Vector2.Dot(mouseRay, playerRay);

                print(direction);

                player.Rotate(direction * 10f);

                // TODO needs simplification
                // TODO remove rotation keybinds, rotation will be handled with mouse now
            }
            else
            {
                if (inputRotation != 0f)
                {
                    player.Rotate(inputRotation);
                }
            }

            player.Animator.SetBool("Moving", inputAddForce);
            uiInvAnimator.SetBool("ShowInv", inputInventory);
        }

        private void UpdateTextCredits() => textCredits.text = credits.ToString();

        // Iterates through inventory slots and disables them if nothing is in them
        private void UpdateInventoryUI()
        {
            UIInventorySlot[] inv = instance.inventory;
            UIInventorySlot slotCurrent;
            ItemInfo itemCurrent;
            int i;

            for (i = 0; i < inv.Length; i++)
            {
                slotCurrent = inv[i];
                itemCurrent = slotCurrent.Item;

                if (itemCurrent == null)
                {
                    slotCurrent.gameObject.SetActive(false);
                }
                else
                {
                    slotCurrent.gameObject.SetActive(true);
                    slotCurrent.Image.color = itemCurrent.Color;
                    slotCurrent.Image.sprite = itemCurrent.Sprite;
                }
            }
        }

        public void CallbackInputAddForce(InputAction.CallbackContext ctx) => inputAddForce = ctx.performed;
        public void CallbackInputFire(InputAction.CallbackContext ctx)
        {
            inputFire = ctx.performed;

            if (inputFire & instance.routineFiring == null)
            {
                instance.routineFiring = StartCoroutine(RoutineFire());
            }
        }
        public void CallbackInputRotate(InputAction.CallbackContext ctx) => inputRotation = ctx.ReadValue<float>();
        public void CallbackInputInventory(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                inputInventory = !inputInventory;
            }
        }
        public void CallbackInputMenu(InputAction.CallbackContext ctx) => inputMenu = ctx.performed;
        public void CallbackMousePosition(InputAction.CallbackContext ctx) => inputMousePosition = ctx.ReadValue<Vector2>();

        public static void GiveCredits(int amount, Vector2 position)
        {
            instance.credits += amount;
            Vector3 pos = position;
            pos.z = -1f;
            TMP_Text textPopup = Instantiate(instance.prefabTextCreditPopup, pos, Quaternion.identity);
            textPopup.text = $"+{amount}";
            instance.UpdateTextCredits();
            Destroy(textPopup.gameObject, 0.5f);
        }

        public static bool GiveItem(ItemInfo item, int amount)
        {
            UIInventorySlot[] inv = instance.inventory;
            int slotFirstEmpty = 0, slotSameItem = 0, i;
            bool foundEmptySlot = false, foundSameItem = false;
            ItemInfo itemCurrent;

            for (i = 0; i < inv.Length & !foundSameItem; i++)
            {
                itemCurrent = inv[i].Item;

                if (itemCurrent == item)
                {
                    foundSameItem = true;
                    continue;
                }
                else
                {
                    ++slotSameItem;
                }

                if (itemCurrent == null)
                {
                    foundEmptySlot = true;
                }
                else
                {
                    ++slotFirstEmpty;
                }
            }

            UIInventorySlot slot;

            if (foundSameItem)
            {
                slot = inv[slotSameItem];
                slot.Amount += amount;
                slot.ChangeSprite();
                instance.UpdateInventoryUI();
                return true;
            }
            else if (foundEmptySlot)
            {
                slot = inv[slotFirstEmpty];
                slot.Item = item;
                slot.Amount = amount;
                slot.ChangeSprite();
                instance.UpdateInventoryUI();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static SpaceObject SpawnSpaceObject(SpaceObjectSettings sos, Vector2 pos)
        {
            SpaceObject r = null;

            // Setup variable for checks
            bool pass = true;

            // If single instance type spawning...
            if (sos.SpawnRateType == SpaceObjectSpawnRateType.SingleInstance)
            {
                // Search through all space objects and see if it's already instantiated
                foreach (SpaceObject so in instance.SpaceObjects)
                {
                    if (so.tag == sos.Tag)
                    {
                        pass = false;
                        break;
                    }
                }
            }

            // If checks passed...
            if (pass)
            {
                // Instantiate space object
                SpaceObject newSpaceObject = sos.RandomSpaceObject;
                Transform parent = instance.parentSpaceObjects;
                Quaternion rot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                SpaceObject spaceObject = Instantiate(newSpaceObject, pos, rot, parent);
                spaceObject.Settings = sos;
                spaceObject.Scale = sos.RandomScale;
                spaceObject.SpriteRenderer.color = sos.Color;

                Vector2 velocity = Util.RandomUnitVector * sos.RandomVelocity;
                float angularVelocity = Random.Range(-1f, 1f) * sos.RandomAngularVelocity;
                spaceObject.Rigidbody.velocity = velocity;
                spaceObject.Rigidbody.angularVelocity = angularVelocity;

                r = spaceObject;
            }

            return r;
        }

        private IEnumerator RoutineSpawnSpaceObject(SpaceObjectSettings sos)
        {
            float waitTime, magnitude;

            while (true)
            {
                // Default wait time
                waitTime = sos.TimeBetweenChance;

                // If scale wait time...
                if (sos.SpawnRateType == SpaceObjectSpawnRateType.ScaleWithMagnitude)
                {
                    magnitude = player.Rigidbody.velocity.magnitude * sos.ScaleSpawnRate;

                    if (magnitude > 1f)
                    {
                        waitTime /= magnitude;
                    }
                }

                // Wait...
                yield return new WaitForSeconds(waitTime);

                // If chance passes...
                if (Random.value <= sos.ChanceSpawn)
                {
                    // Spawn Space Object
                    Transform player = instance.player.transform;
                    Vector2 spawnOffset, spawnPos = player.position;

                    switch (sos.SpawnAreaType)
                    {
                        case SpaceObjectSpawnAreaType.FrontOfPlayer:
                            spawnOffset = player.transform.up * sos.RandomSpawnDistance;
                            spawnOffset += (Vector2)player.transform.right * sos.RandomSpawnWidth;
                            spawnPos += spawnOffset;
                            break;

                        case SpaceObjectSpawnAreaType.AroundPlayer:
                            spawnPos += Util.RandomUnitVector * Random.Range(sos.DistanceSpawn, sos.DistanceMax);
                            break;
                    }

                    SpawnSpaceObject(sos, spawnPos);
                }
            }
        }

        // Routine to clean distance Space Objects
        private IEnumerator RoutineCleanDistantSpaceObjects()
        {
            // Infinite loop...
            while (true)
            {
                // Wait...
                yield return new WaitForSeconds(settings.TimeBetweenCleanup);

                // Check all Space Objects...
                foreach (SpaceObject so in SpaceObjects)
                {
                    // If Space Object too far away...
                    if (Vector2.Distance(player.transform.position, so.transform.position) > so.Settings.DistanceMax)
                    {
                        // Dispose of
                        Destroy(so.gameObject);
                    }
                }
            }
        }

        private IEnumerator RoutineFire()
        {
            while (inputFire)
            {
                player.Fire();

                yield return new WaitForSeconds(player.Stats.TimeBetweenShots);
            }

            routineFiring = null;
        }

        public class Pair<TL, TR>
        {
            public TL Left = default(TL);
            public TR Right = default(TR);
        }
    }
}
