using System.Collections;
using System.Collections.Generic;
using SpaceGame.Items;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using SpaceGame.Utilities;
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
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                throw new System.Exception("GameInfo attempted second instance.");
            }
        }
        #endregion

        [Header("Game", order = 0)]
        [SerializeField] private GameModeSettings settings;
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
        [SerializeField] private GameObject parentCheatMenu;
        [SerializeField] private Image highlightSlot;
        [SerializeField] private Image highlightHotbar;
        [SerializeField] private Image imageHealthBar;
        [SerializeField] private SpaceObjectSettings settingsItemObject;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private List<UIInventorySlot> inventory;
        [SerializeField] private List<UIInventorySlot> invCrafting; // TODO implement
        // TODO when inventory closes, take all items out of invCrafting and put back into inventory

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool doDebugStuff;

        // Cache
        private List<SpaceObject> spaceObjects = new List<SpaceObject>();
        private bool inputAddForce = false;
        private bool inputSlowDown = false;
        private float inputRotation = 0f;
        private Vector2 inputMousePosition = Vector2.zero;
        private Vector2 inputDirection = Vector2.zero;
        private UIInventorySlot selectedSlot = null;
        private UIInventorySlot selectedHotbar = null;
        private RotationType rotationType = RotationType.RotateAxis;

        // Public Getters
        public static bool DO_DEBUG_STUFF => GameInfo.instance.doDebugStuff;
        public static ShipPlayer Player => GameInfo.instance.player;
        public static SpaceObjectSettings SettingsItemObject => GameInfo.instance.settingsItemObject;
        public static Missile PrefabMissile => GameInfo.instance.prefabMissile;

        // Tags
        public static string TagShip => GameInfo.instance.tagShip;
        public static string TagPlayer => GameInfo.instance.tagPlayer;
        public static string TagMissile => GameInfo.instance.tagMissile;

        // Unity Start method
        private void Start()
        {
            System.Array.ForEach(this.settings.SpaceObjectsToSpawn, sos => StartCoroutine(RoutineSpawnSpaceObject(sos)));
            StartCoroutine(RoutineCleanDistantSpaceObjects());
            SelectHotbar(0);
            UpdateInventoryUI();
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
                    this.player.ApplyForce();
                }
            }

            // Rotate Player
            switch (this.rotationType)
            {
                case RotationType.RotateAxis:
                    this.player.Rotate(inputRotation);
                    break;

                case RotationType.AimAtMouse:
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(this.inputMousePosition);
                    this.player.RotateToLookAt(mousePosition);
                    break;

                // TODO needs testing with controller
                case RotationType.AimInDirection:
                    Vector2 direction = this.player.Position + inputDirection;
                    this.player.RotateToLookAt(direction);
                    break;
            }

            // Update Player Animator
            this.player.Animator.SetBool("Moving", this.inputAddForce);

            // Update Info Panel Text
            Rigidbody2D prb = this.player.Rigidbody;
            Vector2 playerPosition = this.player.Position;
            this.textInfoPanel.text = $"pos x: {playerPosition.x}\n" +
                $"pos y: {playerPosition.y}\n" +
                $"velocity: {prb.velocity.magnitude}";

            // Health
            this.imageHealthBar.fillAmount = this.player.Health / this.player.MaxHealth;
        }

        private void UpdateInventoryUI()
        {
            UpdateInventorySlots(this.inventory);
            UpdateInventorySlots(this.invCrafting);
            UpdateSelectedSlot(this.selectedSlot, this.highlightSlot, -1f);
            UpdateSelectedSlot(this.selectedHotbar, this.highlightHotbar, -2f);

            void UpdateInventorySlots(List<UIInventorySlot> slots)
            {
                foreach (UIInventorySlot slot in slots)
                {
                    Item itemCurrent = slot.ItemStack.Item;

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
            }

            void UpdateSelectedSlot(UIInventorySlot selected, Image highlight, float layer)
            {
                if (selected == null)
                {
                    highlight.enabled = false;
                }
                else
                {
                    highlight.rectTransform.SetParent(selected.transform);
                    RectTransform rectHighlight = highlight.rectTransform;
                    RectTransform rectSelected = selected.RectTransform;
                    highlight.transform.localPosition = new Vector3(0f, 0f, layer);
                    highlight.enabled = true;
                }
            }
        }

        public static int GiveItem(ItemStack itemStack)
        {
            Queue<UIInventorySlot> emptySlots = new Queue<UIInventorySlot>();
            List<UIInventorySlot> inv = GameInfo.instance.inventory;
            UIInventorySlot slot;
            Item itemCurrent;

            for (int i = 0; i < inv.Count & itemStack.Amount > 0; ++i)
            {
                slot = inv[i];
                itemCurrent = slot.ItemStack.Item;

                if (itemCurrent == itemStack.Item)
                {
                    // Add amount and update remaining amount
                    itemStack.Amount = slot.AddAmount(itemStack.Amount);
                }
                else if (itemCurrent == null)
                {
                    // Add to empty slot list
                    emptySlots.Enqueue(slot);
                }
            }

            // Fill empty slots with extra
            while (itemStack.Amount > 0 & emptySlots.Count > 0)
            {
                slot = emptySlots.Dequeue();
                slot.ItemStack.Item = itemStack.Item;
                itemStack.Amount = slot.AddAmount(itemStack.Amount);
            }

            GameInfo.instance.UpdateInventoryUI();
            return itemStack.Amount;
        }

        public static void ToggleActive(GameObject obj) => obj.SetActive(!obj.activeSelf);

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
                if (slot.ItemStack.Item != null)
                {
                    // Set slot
                    gi.selectedSlot = slot;
                }
            }
            else
            {
                // Swap items
                ItemStack swap = slot.ItemStack;
                slot.ItemStack = slotSel.ItemStack;
                slotSel.ItemStack = swap;

                // Reset
                gi.selectedSlot = null;
            }

            // Update UI
            gi.UpdateInventoryUI();
        }

        public static void SelectHotbar(int index)
        {
            GameInfo gi = GameInfo.instance;
            gi.selectedHotbar = gi.inventory[index];
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
                foreach (SpaceObject so in gi.spaceObjects)
                {
                    if (so.Settings.Tag == sos.Tag)
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

                spaceObject.Rigidbody.velocity = Util.RandomUnitVector * sos.RandomVelocity;
                spaceObject.Rigidbody.angularVelocity = Random.Range(-1f, 1f) * sos.RandomAngularVelocity;

                gi.spaceObjects.Add(spaceObject);
                r = spaceObject;
            }

            if (GameInfo.DO_DEBUG_STUFF)
            {
                print($"[{sos.name}] Spawned: {pass}");
            }

            return r;
        }

        public static void DestroySpaceObject(SpaceObject spaceObject)
        {
            GameInfo.instance.spaceObjects.Remove(spaceObject);
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

                // Find if chance occurred...
                bool b = Random.value <= soss.ChanceSpawn;

                // If chance passes...
                if (b)
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
                            spawnPos += Util.RandomUnitVector * soss.RandomSpawnDistance;
                            break;
                    }

                    SpawnSpaceObject(soss, spawnPos);
                }

                if (GameInfo.DO_DEBUG_STUFF && soss.DebugAnnounceSpawn)
                {
                    string msg = b ? "SUCCESS" : "FAILURE";
                    print($"[{Time.time:00.0000}] Attempt to spawn [{soss.name}] - {msg}");
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
                this.spaceObjects.ForEach(so =>
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

        #region Input Callbacks
        public void CallbackInputAddForce(InputAction.CallbackContext ctx) => this.inputAddForce = ctx.performed;
        public void CallbackInputSlowDown(InputAction.CallbackContext ctx) => this.inputSlowDown = ctx.performed;
        public void CallbackInputRotate(InputAction.CallbackContext ctx)
        {
            this.rotationType = RotationType.RotateAxis;
            this.inputRotation = ctx.ReadValue<float>();
        }
        public void CallbackInputAimAtMouse(InputAction.CallbackContext ctx)
        {
            this.rotationType = RotationType.AimAtMouse;
            this.inputMousePosition = ctx.ReadValue<Vector2>();
        }
        public void CallbackInputAimInDirection(InputAction.CallbackContext ctx)
        {
            this.rotationType = RotationType.AimInDirection;
            this.inputDirection = ctx.ReadValue<Vector2>();
        }
        public void CallbackInputInventory(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => ToggleActive(this.parentInvUI));
        public void CallbackInputFire(InputAction.CallbackContext ctx) => this.player.IsFiring = ctx.performed;
        public void CallbackInputHotbar1(InputAction.CallbackContext ctx) => SelectHotbar(0);
        public void CallbackInputHotbar2(InputAction.CallbackContext ctx) => SelectHotbar(1);
        public void CallbackInputHotbar3(InputAction.CallbackContext ctx) => SelectHotbar(2);
        public void CallbackInputHotbar4(InputAction.CallbackContext ctx) => SelectHotbar(3);
        public void CallbackInputHotbar5(InputAction.CallbackContext ctx) => SelectHotbar(4);
        public void CallbackInputExit(InputAction.CallbackContext ctx) => Application.Quit();
        public void CallbackInputCheatMenu(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => ToggleActive(this.parentCheatMenu));

        private void OnButtonPress(InputAction.CallbackContext ctx, System.Action action) { if (ctx.performed) { action.Invoke(); } }
        #endregion

        // Button Callbacks
        public void ButtonCraft()
        {
            // TODO craft using items in this.invCraft
            print("ATTEMPTED CRAFTING");
        }

        private enum RotationType
        {
            RotateAxis,     // Rotate Left/Right keys (controller & keyboard)
            AimAtMouse,     // Mouse pointer
            AimInDirection, // Controllers (right stick on controller)
        }
    }
}
