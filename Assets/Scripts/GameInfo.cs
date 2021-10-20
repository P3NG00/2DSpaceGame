using System.Collections;
using System.Collections.Generic;
using SpaceGame.Items;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
using SpaceGame.UI;
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

        [Header("Recipes", order = 3)]
        [SerializeField] private Recipe[] recipes;

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
        [SerializeField] private TMP_Text textInfoPanel;
        [SerializeField] private GameObject parentInvUI;
        [SerializeField] private GameObject parentCheatMenu;
        [SerializeField] private Image highlightSlotSelected;
        [SerializeField] private Image highlightSlotHover;
        [SerializeField] private Image highlightHotbar;
        [SerializeField] private Image imageHealthBar;
        [SerializeField] private SpaceObjectSettings settingsItemObject;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private UIInventorySlot[] slotsInventory;

        [Header("DEBUG", order = 100)]
        [SerializeField] private bool updateUI = false;
        [SerializeField] private bool doDebugRays;
        [SerializeField] private bool doDebugLog;

        // Cache
        private List<SpaceObject> spaceObjects = new List<SpaceObject>();
        private bool inputApplyForce = false;
        private bool inputSlowDown = false;
        private float inputRotation = 0f;
        private Vector2 inputMousePosition = Vector2.zero;
        private Vector2 inputDirection = Vector2.zero;
        private UIInventorySlot selectedSlot;
        private UIInventorySlot hoverSlot;
        private UIInventorySlot selectedHotbar;
        private Enums.RotationType rotationType = Enums.RotationType.RotateAxis;

        // Private Getters
        private UIInventorySlot SlotWeapon => this.slotsInventory[5];

        // Public Getters
        public static ShipPlayer Player => GameInfo.instance.player;
        public static Weapon PlayerWeapon => (Weapon)GameInfo.instance.SlotWeapon.ItemStack.Item;
        public static Missile PrefabMissile => GameInfo.instance.prefabMissile;
        public static SpaceObjectSettings SettingsItemObject => GameInfo.instance.settingsItemObject;
        public static bool DEBUG_RAYS => GameInfo.instance.doDebugRays;
        public static bool DEBUG_LOG => GameInfo.instance.doDebugLog;

        // Tags
        public static string TagShip => GameInfo.instance.tagShip;
        public static string TagPlayer => GameInfo.instance.tagPlayer;
        public static string TagMissile => GameInfo.instance.tagMissile;

        // Unity Start method
        private void Start()
        {
            Application.targetFrameRate = this.framerate;
            System.Array.ForEach(this.settings.SpaceObjectsToSpawn, sos => StartCoroutine(RoutineSpawnSpaceObject(sos)));
            StartCoroutine(RoutineCleanDistantSpaceObjects());
            this.parentInvUI.SetActive(false);
            this.parentCheatMenu.SetActive(false);
            SelectHotbar(0);
            HoverSlot(SlotWeapon, true);
            UpdateInventoryUI();
        }

        // Unity Update method
        private void FixedUpdate()
        {
            // Move Player
            this.player.ApplyDrag(this.inputSlowDown);

            // TODO test movement, slowdown and applyforce
            if (!this.inputSlowDown & this.inputApplyForce)
            {
                this.player.ApplyForce();
            }

            // Rotate Player
            switch (this.rotationType)
            {
                case Enums.RotationType.RotateAxis:
                    this.player.Rotate(inputRotation);
                    break;

                case Enums.RotationType.AimAtMouse:
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(this.inputMousePosition);
                    this.player.RotateToLookAt(mousePosition);
                    break;

                case Enums.RotationType.AimInDirection:
                    Vector2 direction = this.player.Position + inputDirection;
                    this.player.RotateToLookAt(direction);
                    break;
            }

            // Update Player Animator
            this.player.Animator.SetBool("Moving", this.inputApplyForce);

            // Update Info Panel Text
            this.textInfoPanel.text =
                $"pos x: {this.player.Position.x}\n" +
                $"pos y: {this.player.Position.y}\n" +
                $"velocity: {this.player.Rigidbody.velocity.magnitude}";

            // Health
            this.imageHealthBar.fillAmount = this.player.Health / this.player.MaxHealth;

            // UI
            if (this.updateUI)
            {
                this.updateUI = false;

                // Update slot visual info
                foreach (UIInventorySlot slot in this.slotsInventory)
                {
                    ItemStack slotStack = slot.ItemStack;
                    bool hasItem = slotStack.Item != null;

                    if (hasItem)
                    {
                        if (slotStack.Amount <= 0)
                        {
                            slotStack.Item = null;
                            hasItem = false;
                        }
                        else
                        {
                            slot.Image.color = slotStack.Item.Color;
                            slot.Image.sprite = slotStack.Item.Sprite;
                            slot.UpdateText();
                        }
                    }

                    slot.SetVisible(hasItem);
                }

                this.highlightSlotHover.gameObject.SetActive(this.parentInvUI.activeSelf);

                UpdateSelectedSlot(this.selectedSlot, this.highlightSlotSelected, -1f);
                UpdateSelectedSlot(this.hoverSlot, this.highlightSlotHover, -2f);
                UpdateSelectedSlot(this.selectedHotbar, this.highlightHotbar, -3f);

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
        }

        public static void UpdateInventoryUI() => GameInfo.instance.updateUI = true;

        public static int GiveItem(ItemStack itemStack)
        {
            Queue<UIInventorySlot> emptySlots = new Queue<UIInventorySlot>();
            UIInventorySlot[] inv = GameInfo.instance.slotsInventory;
            UIInventorySlot slot;
            Item itemCurrent;

            for (int i = 0; i < inv.Length & itemStack.Amount > 0; ++i)
            {
                slot = inv[i];
                itemCurrent = slot.ItemStack.Item;

                if (itemCurrent == itemStack.Item)
                {
                    // Add amount and update remaining amount
                    slot.ItemStack.Amount += itemStack.Amount;
                    itemStack.Amount = slot.ItemStack.AddAmount(itemStack.Amount);
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
                itemStack.Amount = slot.ItemStack.AddAmount(itemStack.Amount);
            }

            UpdateInventoryUI();
            return itemStack.Amount;
        }

        // TODO needs testing
        public static bool RemoveItemFromSlots(UIInventorySlot[] invSlots, ItemStack itemStack)
        {
            ItemStack slotStack;

            for (int i = 0; i < invSlots.Length & itemStack.Amount != 0; i++)
            {
                slotStack = invSlots[i].ItemStack;

                if (slotStack.Item == itemStack.Item)
                {
                    slotStack.Amount = Mathf.Abs(slotStack.AddAmount(-itemStack.Amount));
                }
            }

            UpdateInventoryUI();
            return itemStack.Amount == 0;
        }

        public static void SelectSlot(UIInventorySlot slot)
        {
            if (slot != null)
            {
                GameInfo gi = GameInfo.instance;

                // If selecting selected slot...
                if (gi.selectedSlot == slot)
                {
                    // Deselect slot
                    gi.selectedSlot = null;
                    UpdateInventoryUI();
                }
                // Else if no slot selected...
                else if (gi.selectedSlot == null)
                {
                    // If slot stack has item...
                    if (slot.ItemStack.Item != null)
                    {
                        // Select slot
                        gi.selectedSlot = slot;
                        UpdateInventoryUI();
                    }
                }
                // Else, selected slot exists and is selecting different slot...
                else
                {
                    // If selecting weapon slot...
                    if (slot == gi.SlotWeapon)
                    {
                        // If item being moved is a weapon...
                        if (gi.selectedSlot.ItemStack.Item is Weapon)
                        {
                            SwapItems();
                        }
                    }
                    else
                    {
                        SwapItems();
                    }

                    void SwapItems()
                    {
                        // Swap items
                        ItemStack swap = slot.ItemStack;
                        slot.ItemStack = gi.selectedSlot.ItemStack;
                        gi.selectedSlot.ItemStack = swap;

                        // Reset
                        gi.selectedSlot = null;
                        UpdateInventoryUI();
                    }
                }
            }
        }

        public static void HoverSlot(UIInventorySlot slot, bool ovrd = false)
        {
            if (slot != null & (ovrd | GameInfo.instance.parentInvUI.activeSelf))
            {
                GameInfo.instance.hoverSlot = slot;
                UpdateInventoryUI();
            }
        }

        public static void SelectHotbar(int index)
        {
            GameInfo.instance.selectedHotbar = GameInfo.instance.slotsInventory[index];
            UpdateInventoryUI();
        }

        public static SpaceObject SpawnSpaceObject(SpaceObjectSettings sos, Vector2 pos)
        {
            GameInfo gi = GameInfo.instance;
            SpaceObject r = null;
            bool pass = true;

            // If single instance type spawning...
            if (sos is SpaceObjectSpawnableSettings soss && soss.SpawnRateType == Enums.SpaceObjectSpawnRateType.SingleInstance)
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

            if (GameInfo.DEBUG_LOG)
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
                if (soss.SpawnRateType == Enums.SpaceObjectSpawnRateType.ScaleWithMagnitude)
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
                bool chancePassed = Random.value <= soss.ChanceSpawn;

                // If chance passes...
                if (chancePassed)
                {
                    // Spawn Space Object
                    Transform transformPlayer = this.player.transform;
                    Vector2 spawnOffset, spawnPos = transformPlayer.position;

                    switch (soss.SpawnAreaType)
                    {
                        case Enums.SpaceObjectSpawnAreaType.FrontOfPlayer:
                            spawnOffset = transformPlayer.up * soss.RandomSpawnDistance;
                            spawnOffset += (Vector2)transformPlayer.right * soss.RandomSpawnWidth;
                            spawnPos += spawnOffset;
                            break;

                        case Enums.SpaceObjectSpawnAreaType.AroundPlayer:
                            spawnPos += Util.RandomUnitVector * soss.RandomSpawnDistance;
                            break;
                    }

                    SpawnSpaceObject(soss, spawnPos);
                }

                if (GameInfo.DEBUG_LOG && soss.DebugAnnounceSpawn)
                {
                    print($"[{Time.time:00.0000}] Attempt to spawn [{soss.name}] - {(chancePassed ? "SUCCESS" : "FAILURE")}");
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
                foreach (SpaceObject so in this.spaceObjects)
                {
                    // If Space Object too far away...
                    if (Vector2.Distance(this.player.transform.position, so.transform.position) > so.Settings.DistanceMax)
                    {
                        // Add to disposal list
                        objectsToRemove.Add(so);
                    }
                }

                // Remove Space Objects
                objectsToRemove.ForEach(so => DestroySpaceObject(so));
                objectsToRemove.Clear();
            }
        }

        #region Button Callbacks
        public void CallbackButton_CheatToggleInvincible() => Util.ToggleBool(ref this.player.Invincible);
        public void CallbackButton_CheatFullHeal() => this.player.Heal(this.player.MaxHealth);
        #endregion

        #region Input Callbacks
        public void CallbackInput_AddForce(InputAction.CallbackContext ctx) => this.inputApplyForce = ctx.performed;
        public void CallbackInput_AimAtMouse(InputAction.CallbackContext ctx)
        {
            this.rotationType = Enums.RotationType.AimAtMouse;
            this.inputMousePosition = ctx.ReadValue<Vector2>();
        }
        public void CallbackInput_AimInDirection(InputAction.CallbackContext ctx)
        {
            this.rotationType = Enums.RotationType.AimInDirection;
            this.inputDirection = ctx.ReadValue<Vector2>();
        }
        public void CallbackInput_CheatMenu(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => Util.ToggleActive(this.parentCheatMenu));
        public void CallbackInput_Exit(InputAction.CallbackContext ctx) => Application.Quit();
        public void CallbackInput_Fire(InputAction.CallbackContext ctx) => this.player.IsFiring = ctx.performed;
        public void CallbackInput_Hotbar1(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => SelectHotbar(0));
        public void CallbackInput_Hotbar2(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => SelectHotbar(1));
        public void CallbackInput_Hotbar3(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => SelectHotbar(2));
        public void CallbackInput_Hotbar4(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => SelectHotbar(3));
        public void CallbackInput_Hotbar5(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => SelectHotbar(4));
        public void CallbackInput_Inventory(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () =>
        {
            Util.ToggleActive(this.parentInvUI);
            UpdateInventoryUI();
        });
        public void CallbackInput_MenuDown(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => this.hoverSlot.NextSlot(Enums.Direction.Down));
        public void CallbackInput_MenuLeft(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => this.hoverSlot.NextSlot(Enums.Direction.Left));
        public void CallbackInput_MenuRight(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => this.hoverSlot.NextSlot(Enums.Direction.Right));
        public void CallbackInput_MenuUp(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => this.hoverSlot.NextSlot(Enums.Direction.Up));
        public void CallbackInput_Rotate(InputAction.CallbackContext ctx)
        {
            this.rotationType = Enums.RotationType.RotateAxis;
            this.inputRotation = ctx.ReadValue<float>();
        }
        public void CallbackInput_SelectSlot(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.SelectSlot(this.hoverSlot));
        public void CallbackInput_SlowDown(InputAction.CallbackContext ctx) => this.inputSlowDown = ctx.performed;

        private void OnButtonPress(InputAction.CallbackContext ctx, System.Action action) { if (ctx.performed) { action.Invoke(); } }
        #endregion
    }
}
