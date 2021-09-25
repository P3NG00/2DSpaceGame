using System.Security.AccessControl;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        // TODO add health
        [SerializeField] private int credits;
        [SerializeField] private GameModeSettings settings;

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("Tags", order = 5)]
        [SerializeField] private string tagPlayer;
        [SerializeField] private string tagMissile;

        [Header("Prefabs", order = 50)]
        [SerializeField] private Rigidbody2D prefabMissile;
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        [Header("References", order = 99)]
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceObjects;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text textInfoPanel;
        [SerializeField] private GameObject parentInvUI;
        [SerializeField] private Image slotHighlight;
        [SerializeField] private SpaceObjectSettings settingsItemObject;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private List<UIInventorySlot> inventory;

        private List<SpaceObject> SpaceObjects = new List<SpaceObject>();

        // TODO add input slow down
        private bool inputAddForce = false;
        private bool inputFire = false;
        private bool inputMenu = false;
        private Vector2 inputMousePosition = Vector2.zero;
        private Coroutine routineFiring = null;
        private UIInventorySlot selectedSlot;

        public static GameModeSettings GMSettings => instance.settings;
        public static SpaceObjectSettings SettingsItemObject => instance.settingsItemObject;
        public static Sprite[] Sprites => instance.sprites;

        public static string TagPlayer => instance.tagPlayer;
        public static string TagMissile => instance.tagMissile;

        public static Rigidbody2D PrefabMissile => instance.prefabMissile;

        public static Vector2 RandomUnitVector => Random.insideUnitCircle.normalized;

        // Unity Start method
        private void Start()
        {
            foreach (SpaceObjectSpawnableSettings sos in settings.SpaceObjectsToSpawn)
            {
                StartCoroutine(RoutineSpawnSpaceObject(sos));
            }

            StartCoroutine(RoutineCleanDistantSpaceObjects());
            UpdateTextCredits();
            UpdateInventoryUI();
            ToggleInventory();
            Application.targetFrameRate = framerate;
        }

        // Unity Update method
        private void FixedUpdate()
        {
            if (inputAddForce)
            {
                player.AddForce();
            }

            Vector2 playerPosition = player.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputMousePosition);

            Vector2 mouseOffset = mousePosition - playerPosition;

            // Draw rays to display in editor
            Debug.DrawLine(playerPosition, playerPosition + ((Vector2)player.transform.up * mouseOffset.magnitude), Color.magenta);
            Debug.DrawLine(playerPosition, mousePosition, Color.white);

            float direction = Vector2.Dot(mouseOffset.normalized, player.transform.right);
            player.Rotate(direction);

            player.Animator.SetBool("Moving", inputAddForce);

            textInfoPanel.text = $"x: {playerPosition.x}\n" +
                $"y: {playerPosition.y}\n" +
                $"magnitude: {player.Rigidbody.velocity.magnitude}\n" +
                $"angular velocity: {player.Rigidbody.angularVelocity}";
        }

        private void UpdateTextCredits()
        {
            textCredits.text = credits.ToString();
        }

        private void UpdateInventoryUI()
        {
            var inv = instance.inventory;
            UIInventorySlot slotCurrent;
            ItemInfo itemCurrent;

            for (int i = 0; i < inv.Count; i++)
            {
                slotCurrent = inv[i];
                itemCurrent = slotCurrent.Item;

                if (itemCurrent == null)
                {
                    slotCurrent.SetVisible(false);
                }
                else
                {
                    slotCurrent.Image.color = itemCurrent.Color;
                    slotCurrent.Image.sprite = itemCurrent.Sprite;
                    slotCurrent.SetVisible(true);
                    slotCurrent.UpdateText();
                }
            }

            if (selectedSlot == null)
            {
                slotHighlight.enabled = false;
            }
            else
            {
                // TODO move highlight slot to center of slot properly
                slotHighlight.rectTransform.SetParent(selectedSlot.transform);

                RectTransform rectH = slotHighlight.rectTransform;
                RectTransform rectS = selectedSlot.RectTransform;

                slotHighlight.transform.localPosition = new Vector3(0f, 0f, -5f);

                slotHighlight.enabled = true;
            }
        }

        public static void GiveCredits(int amount, Vector2 position)
        {
            GameInfo gi = GameInfo.instance;

            // Add credits
            gi.credits += amount;

            // Position to spawn
            Vector3 pos = position;
            pos.z = -1f;

            // Instantiate Text Object
            TMP_Text textPopup = Instantiate(gi.prefabTextCreditPopup, pos, Quaternion.identity);
            textPopup.text = $"+{amount}";
            Destroy(textPopup.gameObject, 0.5f);

            // Update Credits Text
            gi.UpdateTextCredits();
        }

        public static bool GiveItem(ItemInfo item, int amount)
        {
            var inv = instance.inventory;
            int slotFirstEmpty = 0, slotSameItem = 0;
            bool foundEmptySlot = false, foundSameItem = false;
            ItemInfo itemCurrent;

            for (int i = 0; i < inv.Count; i++)
            {
                itemCurrent = inv[i].Item;

                if (itemCurrent == item)
                {
                    foundSameItem = true;
                    break;
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
            }
            else if (foundEmptySlot)
            {
                slot = inv[slotFirstEmpty];
                slot.Item = item;
                slot.Amount = amount;
            }
            else
            {
                return false;
            }

            slot.UpdateText();
            instance.UpdateInventoryUI();
            return true;
        }

        public static void ToggleInventory()
        {
            GameObject ui = instance.parentInvUI;
            ui.SetActive(!ui.activeSelf);
        }

        public static void SelectSlot(UIInventorySlot slot)
        {
            GameInfo gi = GameInfo.instance;
            UIInventorySlot slotSel = gi.selectedSlot;

            // If no slot selected...
            if (slotSel == null)
            {
                if (slot.Item != null)
                {
                    // Set slot
                    gi.selectedSlot = slot;
                }
            }
            else
            {
                // Swap items
                ItemInfo swapItem = slot.Item;
                int swapAmount = slot.Amount;

                slot.Item = slotSel.Item;
                slot.Amount = slotSel.Amount;

                slotSel.Item = swapItem;
                slotSel.Amount = swapAmount;

                // Reset
                gi.selectedSlot = null;
            }

            // Update UI
            gi.UpdateInventoryUI();
        }

        public static SpaceObject SpawnSpaceObject(SpaceObjectSettings sos, Vector2 pos)
        {
            SpaceObject r = null;

            // Setup variable for checks
            bool pass = true;

            // If single instance type spawning...
            if (sos is SpaceObjectSpawnableSettings soss && soss.SpawnRateType == SpaceObjectSpawnRateType.SingleInstance)
            {
                // Search through all space objects and see if it's already instantiated
                foreach (SpaceObject so in instance.SpaceObjects)
                {
                    if (so.tag == sos.Tag)
                    {
                        // Cannot pass
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

                Vector2 velocity = RandomUnitVector * sos.RandomVelocity;
                float angularVelocity = Random.Range(-1f, 1f) * sos.RandomAngularVelocity;
                spaceObject.Rigidbody.velocity = velocity;
                spaceObject.Rigidbody.angularVelocity = angularVelocity;

                instance.SpaceObjects.Add(spaceObject);
                r = spaceObject;
            }

            return r;
        }

        public static void DestroySpaceObject(SpaceObject spaceObject)
        {
            instance.SpaceObjects.Remove(spaceObject);
            Destroy(spaceObject.gameObject);
        }

        private IEnumerator RoutineSpawnSpaceObject(SpaceObjectSpawnableSettings soss)
        {
            float waitTime, magnitude;

            while (true)
            {
                // Default wait time
                waitTime = soss.TimeBetweenChance;

                // If scale wait time...
                if (soss.SpawnRateType == SpaceObjectSpawnRateType.ScaleWithMagnitude)
                {
                    magnitude = player.Rigidbody.velocity.magnitude * soss.ScaleSpawnRate;

                    if (magnitude > 1f)
                    {
                        waitTime /= magnitude;
                    }
                }

                // Wait...
                yield return new WaitForSeconds(waitTime);

                // If chance passes...
                if (Random.value <= soss.ChanceSpawn)
                {
                    // Spawn Space Object
                    Transform player = instance.player.transform;
                    Vector2 spawnOffset, spawnPos = player.position;

                    switch (soss.SpawnAreaType)
                    {
                        case SpaceObjectSpawnAreaType.FrontOfPlayer:
                            spawnOffset = player.transform.up * soss.RandomSpawnDistance;
                            spawnOffset += (Vector2)player.transform.right * soss.RandomSpawnWidth;
                            spawnPos += spawnOffset;
                            break;

                        case SpaceObjectSpawnAreaType.AroundPlayer:
                            spawnPos += RandomUnitVector * Random.Range(soss.DistanceSpawn, soss.DistanceMax);
                            break;
                    }

                    SpawnSpaceObject(soss, spawnPos);
                }
            }
        }

        private IEnumerator RoutineCleanDistantSpaceObjects()
        {
            List<SpaceObject> objectsToRemove = new List<SpaceObject>();

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
                        // Add to disposal list
                        objectsToRemove.Add(so);
                    }
                }

                // Remove Space Objects
                objectsToRemove.ForEach(so => DestroySpaceObject(so));

                // Reset
                objectsToRemove.Clear();
            }
        }

        private IEnumerator RoutineFire()
        {
            while (inputFire)
            {
                player.Fire();

                yield return new WaitForSeconds(player.Stats.Weapon.TimeBetweenShots);
            }

            routineFiring = null;
        }

        #region Input Callbacks
        public void CallbackInputAddForce(InputAction.CallbackContext ctx) => inputAddForce = ctx.performed;
        public void CallbackMousePosition(InputAction.CallbackContext ctx) => inputMousePosition = ctx.ReadValue<Vector2>();
        public void CallbackInputMenu(InputAction.CallbackContext ctx) => inputMenu = ctx.performed;

        public void CallbackInputFire(InputAction.CallbackContext ctx)
        {
            inputFire = ctx.performed;

            if (inputFire & instance.routineFiring == null)
            {
                instance.routineFiring = StartCoroutine(RoutineFire());
            }
        }

        public void CallbackInputInventory(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                ToggleInventory();
            }
        }
        #endregion
    }
}
