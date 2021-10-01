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
            if (GameInfo.instance == null)
            {
                GameInfo.instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                throw new System.Exception("GameInfo attempted second instance.");
            }
        }
        #endregion

        [Header("Game", order = 0)]
        [SerializeField] private int credits;
        [SerializeField] private GameModeSettings settings;

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("Tags", order = 5)]
        [SerializeField] private string tagShip;
        [SerializeField] private string tagPlayer;
        [SerializeField] private string tagMissile;

        [Header("Prefabs", order = 50)]
        [SerializeField] private Missile prefabMissile;
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        [Header("References", order = 99)]
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceObjects;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text textInfoPanel;
        [SerializeField] private GameObject parentInvUI;
        [SerializeField] private Image slotHighlight;
        [SerializeField] private Image imageHealthBar;
        [SerializeField] private SpaceObjectSettings settingsItemObject;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private List<UIInventorySlot> inventory;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool doDebugStuff;
        [SerializeField] private Color colorPointing;
        [SerializeField] private Color colorFacing;

        // Cache
        private List<SpaceObject> SpaceObjects = new List<SpaceObject>();
        private bool inputAddForce = false;
        private bool inputSlowDown = false;
        private bool inputFire = false;
        private bool inputMenu = false;
        private Vector2 inputMousePosition = Vector2.zero;
        private Coroutine routineFiring = null;
        private UIInventorySlot selectedSlot;

        // Public Getters
        public static bool DO_DEBUG_STUFF => GameInfo.instance.doDebugStuff;
        public static GameModeSettings GMSettings => GameInfo.instance.settings;
        public static SpaceObjectSettings SettingsItemObject => GameInfo.instance.settingsItemObject;
        public static Sprite[] Sprites => GameInfo.instance.sprites;
        public static Missile PrefabMissile => GameInfo.instance.prefabMissile;

        // Tags
        public static string TagShip => GameInfo.instance.tagShip;
        public static string TagPlayer => GameInfo.instance.tagPlayer;
        public static string TagMissile => GameInfo.instance.tagMissile;

        // Util
        public static Vector2 RandomUnitVector => Random.insideUnitCircle.normalized;

        // Unity Start method
        private void Start()
        {
            System.Array.ForEach(this.settings.SpaceObjectsToSpawn, sos => StartCoroutine(RoutineSpawnSpaceObject(sos)));
            StartCoroutine(RoutineCleanDistantSpaceObjects());
            UpdateTextCredits();
            UpdateInventoryUI();
            ToggleInventory();
            Application.targetFrameRate = this.framerate;
        }

        // Unity Update method
        private void FixedUpdate()
        {
            // Move Player
            if (this.inputSlowDown)
            {
                this.player.ApplyDrag(true);
            }
            else
            {
                this.player.ApplyDrag(false);

                if (this.inputAddForce)
                {
                    this.player.AddForce();
                }
            }

            // Variables to make player look at mouse
            Vector2 playerPosition = this.player.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(this.inputMousePosition);

            Vector2 mouseOffset = mousePosition - playerPosition;

            if (DO_DEBUG_STUFF)
            {
                // Draw rays to display in editor
                Debug.DrawLine(playerPosition, playerPosition + ((Vector2)this.player.transform.up * mouseOffset.magnitude), this.colorFacing);
                Debug.DrawLine(playerPosition, mousePosition, this.colorPointing);
            }

            // Rotate Player
            float direction = Vector2.Dot(mouseOffset.normalized, this.player.transform.right);
            this.player.Rotate(direction);

            // Update Player Animator
            this.player.Animator.SetBool("Moving", this.inputAddForce);

            // Update Info Panel Text
            Rigidbody2D prb = this.player.Rigidbody;
            this.textInfoPanel.text = $"x: {playerPosition.x}\n" +
                $"y: {playerPosition.y}\n" +
                $"magnitude: {prb.velocity.magnitude}\n" +
                $"angular velocity: {prb.angularVelocity}";

            // Health
            this.imageHealthBar.fillAmount = this.player.Health / this.player.MaxHealth;
        }

        private void UpdateTextCredits()
        {
            this.textCredits.text = this.credits.ToString();
        }

        private void UpdateInventoryUI()
        {
            List<UIInventorySlot> inv = GameInfo.instance.inventory;
            UIInventorySlot slot;
            ItemInfo itemCurrent;

            for (int i = 0; i < inv.Count; i++)
            {
                slot = inv[i];
                itemCurrent = slot.Item;

                if (itemCurrent == null)
                {
                    slot.SetVisible(false);
                }
                else
                {
                    slot.Image.color = itemCurrent.Color;
                    slot.Image.sprite = itemCurrent.Sprite;
                    slot.SetVisible(true);
                    slot.UpdateText();
                }
            }

            if (selectedSlot == null)
            {
                this.slotHighlight.enabled = false;
            }
            else
            {
                this.slotHighlight.rectTransform.SetParent(this.selectedSlot.transform);

                RectTransform rectH = this.slotHighlight.rectTransform;
                RectTransform rectS = this.selectedSlot.RectTransform;

                this.slotHighlight.transform.localPosition = new Vector3(0f, 0f, -5f);
                this.slotHighlight.enabled = true;
            }
        }

        public static void ValidateMinMax(float min, ref float max) { if (min > max) { max = min; } }

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

        public static int GiveItem(ItemInfo item, int amount)
        {
            Queue<UIInventorySlot> emptySlots = new Queue<UIInventorySlot>();
            List<UIInventorySlot> inv = GameInfo.instance.inventory;
            UIInventorySlot slot;
            ItemInfo itemCurrent;

            for (int i = 0; i < inv.Count & amount > 0; ++i)
            {
                slot = inv[i];
                itemCurrent = slot.Item;

                if (itemCurrent == item)
                {
                    // Add amount and update remaining amount
                    amount = slot.AddAmount(amount);
                }
                else if (itemCurrent == null)
                {
                    // Add to empty slot list
                    emptySlots.Enqueue(slot);
                }
            }

            // Fill empty slots with extra
            while (amount > 0 & emptySlots.Count > 0)
            {
                slot = emptySlots.Dequeue();
                slot.Item = item;
                amount = slot.AddAmount(amount);
            }

            GameInfo.instance.UpdateInventoryUI();
            return amount;
        }

        public static void ToggleInventory()
        {
            GameObject ui = GameInfo.instance.parentInvUI;
            ui.SetActive(!ui.activeSelf);
        }

        public static void SelectSlot(UIInventorySlot slot)
        {
            GameInfo gi = GameInfo.instance;
            UIInventorySlot slotSel = gi.selectedSlot;

            // If no slot selected...
            if (slotSel == slot)
            {
                gi.selectedSlot = null;
            }
            else if (slotSel == null)
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
            GameInfo gi = GameInfo.instance;
            SpaceObject r = null;
            bool pass = true;

            // If single instance type spawning...
            if (sos is SpaceObjectSpawnableSettings soss && soss.SpawnRateType == SpaceObjectSpawnRateType.SingleInstance)
            {
                // Search through all space objects and see if it's already instantiated
                foreach (SpaceObject so in gi.SpaceObjects)
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
                Quaternion rot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                SpaceObject spaceObject = Instantiate(sos.RandomSpaceObject, pos, rot, gi.parentSpaceObjects);
                spaceObject.Settings = sos;
                spaceObject.Scale = sos.RandomScale;
                spaceObject.SpriteRenderer.color = sos.Color;

                spaceObject.Rigidbody.velocity = RandomUnitVector * sos.RandomVelocity;
                spaceObject.Rigidbody.angularVelocity = Random.Range(-1f, 1f) * sos.RandomAngularVelocity;

                gi.SpaceObjects.Add(spaceObject);
                r = spaceObject;
            }

            return r;
        }

        public static void DestroySpaceObject(SpaceObject spaceObject)
        {
            GameInfo.instance.SpaceObjects.Remove(spaceObject);
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
                    magnitude = this.player.Rigidbody.velocity.magnitude * soss.ScaleSpawnRate;

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
                    Transform transformPlayer = this.player.transform;
                    Vector2 spawnOffset, spawnPos = transformPlayer.position;

                    switch (soss.SpawnAreaType)
                    {
                        case SpaceObjectSpawnAreaType.FrontOfPlayer:
                            spawnOffset = transformPlayer.up * soss.RandomSpawnDistance;
                            spawnOffset += (Vector2)transformPlayer.right * soss.RandomSpawnWidth;
                            spawnPos += spawnOffset;
                            break;

                        case SpaceObjectSpawnAreaType.AroundPlayer:
                            spawnPos += RandomUnitVector * soss.RandomSpawnDistance;
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
                yield return new WaitForSeconds(this.settings.TimeBetweenCleanup);

                // Check all Space Objects...
                this.SpaceObjects.ForEach(so =>
                {
                    // If Space Object too far away...
                    if (Vector2.Distance(this.player.transform.position, so.transform.position) > so.Settings.DistanceMax)
                    {
                        // Add to disposal list
                        objectsToRemove.Add(so);
                    }
                });

                // Remove Space Objects
                objectsToRemove.ForEach(so => DestroySpaceObject(so));
                objectsToRemove.Clear();
            }
        }

        private IEnumerator RoutineFire()
        {
            while (this.inputFire)
            {
                this.player.Fire();
                yield return new WaitForSeconds(this.player.Weapon.TimeBetweenShots);
            }

            this.routineFiring = null;
        }

        #region Input Callbacks
        public void CallbackInputAddForce(InputAction.CallbackContext ctx) => this.inputAddForce = ctx.performed;
        public void CallbackInputSlowDown(InputAction.CallbackContext ctx) => this.inputSlowDown = ctx.performed;
        public void CallbackMousePosition(InputAction.CallbackContext ctx) => this.inputMousePosition = ctx.ReadValue<Vector2>();
        public void CallbackInputMenu(InputAction.CallbackContext ctx) => this.inputMenu = ctx.performed;
        public void CallbackInputInventory(InputAction.CallbackContext ctx) { if (ctx.performed) { ToggleInventory(); } }
        public void CallbackInputFire(InputAction.CallbackContext ctx)
        {
            this.inputFire = ctx.performed;

            if (this.inputFire & this.routineFiring == null)
            {
                this.routineFiring = StartCoroutine(RoutineFire());
            }
        }
        #endregion
    }
}
