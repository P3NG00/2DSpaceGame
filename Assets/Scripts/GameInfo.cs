using System.Collections;
using System.Collections.Generic;
using SpaceGame.Items;
using SpaceGame.Projectiles;
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
        [SerializeField] private GameModeInfo settings;
        [SerializeField] private int framerate;

        [Header("Tags", order = 5)]
        [SerializeField] private string tagShip;
        [SerializeField] private string tagPlayer;
        [SerializeField] private string tagMissile;

        [Header("Prefabs", order = 50)]
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        [Header("References", order = 99)]
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceObjects;
        [SerializeField] private TMP_Text textInfoPanel;
        [SerializeField] private GameObject parentInvUI;
        [SerializeField] private GameObject parentCheatMenu;
        [SerializeField] private Image highlightSlotSelected;
        [SerializeField] private Image highlightHoverSlot;
        [SerializeField] private Image highlightSelectedHotbar;
        [SerializeField] private Image imageHealthBar;
        [SerializeField] private SpaceObjectInfo settingsItemObject;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private UIInventorySlot[] slotsInventory;
        [SerializeField] private UIInventorySlot[] slotsHotbar;
        [SerializeField] private UIInventorySlot slotWeapon;

        [Header("DEBUG", order = 100)]
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
        private List<UIInventorySlot> invSlots = new List<UIInventorySlot>();
        private List<UIInventorySlot> allSlots = new List<UIInventorySlot>();

        // Public Getters
        public static ShipPlayer Player => GameInfo.instance.player;
        public static SpaceObjectInfo SettingsItemObject => GameInfo.instance.settingsItemObject;
        public static bool DEBUG_RAYS => GameInfo.instance.doDebugRays;
        public static bool DEBUG_LOG => GameInfo.instance.doDebugLog;
        public static UIInventorySlot SlotWeapon => GameInfo.instance.slotWeapon;
        public static GameObject ParentCheatMenu => GameInfo.instance.parentCheatMenu;
        public static ItemInfoProjectile PlayerWeaponInfo
        {
            get
            {
                ItemInfo itemInfo = GameInfo.SlotWeapon.ItemStack.ItemInfo;
                return itemInfo == null ? null : (ItemInfoProjectile)itemInfo;
            }
        }

        // Tags
        public static string TagShip => GameInfo.instance.tagShip;
        public static string TagPlayer => GameInfo.instance.tagPlayer;
        public static string TagMissile => GameInfo.instance.tagMissile;

        // Unity Start method
        private void Start()
        {
            this.invSlots.AddRange(this.slotsHotbar);
            this.invSlots.AddRange(this.slotsInventory);
            this.allSlots.AddRange(this.slotsInventory);
            this.allSlots.AddRange(this.slotsHotbar);
            this.allSlots.Add(this.slotWeapon);
            Application.targetFrameRate = this.framerate;
            System.Array.ForEach(this.settings.SpaceObjectsToSpawn, sos => StartCoroutine(RoutineSpawnSpaceObject(sos)));
            StartCoroutine(RoutineCleanDistantSpaceObjects());
            this.parentInvUI.SetActive(false);
            this.parentCheatMenu.SetActive(false);
            SelectHotbar(0);
            HoverSlot(SlotWeapon, true);
        }

        // Unity Update method
        private void FixedUpdate()
        {
            // Move Player
            this.player.ApplyDrag(this.inputSlowDown);

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

            // Update UI
            foreach (UIInventorySlot slot in this.allSlots)
            {
                ItemStack slotStack = slot.ItemStack;
                bool hasItem = slotStack.ItemInfo != null;

                if (hasItem)
                {
                    if (slotStack.Amount <= 0)
                    {
                        slotStack.ItemInfo = null;
                        hasItem = false;
                    }
                    else
                    {
                        slot.Image.color = slotStack.ItemInfo.Color;
                        slot.Image.sprite = slotStack.ItemInfo.Sprite;
                    }
                }

                slot.Visible = hasItem;
                slot.UpdateText();
            }

            this.highlightHoverSlot.gameObject.SetActive(this.parentInvUI.activeSelf);

            UpdateSelectedSlot(this.selectedSlot, this.highlightSlotSelected, -1);
            UpdateSelectedSlot(this.hoverSlot, this.highlightHoverSlot, -2);
            UpdateSelectedSlot(this.selectedHotbar, this.highlightSelectedHotbar, -3);

            void UpdateSelectedSlot(UIInventorySlot selected, Image highlight, int layer)
            {
                bool b = selected != null;
                highlight.enabled = b;

                if (b)
                {
                    highlight.rectTransform.SetParent(selected.transform);
                    RectTransform rectHighlight = highlight.rectTransform;
                    RectTransform rectSelected = selected.RectTransform;
                    highlight.transform.localPosition = new Vector3(0f, 0f, layer);
                }
            }
        }

        public static int GiveItem(ItemStack itemStack)
        {
            Queue<UIInventorySlot> emptySlots = new Queue<UIInventorySlot>();
            List<UIInventorySlot> inv = GameInfo.instance.invSlots;
            UIInventorySlot slot;
            ItemInfo itemCurrent;

            for (int i = 0; i < inv.Count & itemStack.Amount > 0; ++i)
            {
                slot = inv[i];
                itemCurrent = slot.ItemStack.ItemInfo;

                if (itemCurrent == itemStack.ItemInfo)
                {
                    // Add amount and update remaining amount
                    itemStack.Amount = slot.ItemStack.ModifyAmount(itemStack.Amount);
                }
                else if (itemCurrent == null)
                {
                    // Add to empty slot list
                    emptySlots.Enqueue(slot);
                }
            }

            // Fill empty slots with extra
            while (itemStack.Amount > 0 && emptySlots.Count > 0)
            {
                slot = emptySlots.Dequeue();
                slot.ItemStack.ItemInfo = itemStack.ItemInfo;
                itemStack.Amount = slot.ItemStack.ModifyAmount(itemStack.Amount);
            }

            return itemStack.Amount;
        }

        // TODO needs testing
        public static bool RemoveItemFromSlots(UIInventorySlot[] invSlots, ItemStack itemStack)
        {
            ItemStack slotStack;

            for (int i = 0; i < invSlots.Length & itemStack.Amount != 0; i++)
            {
                slotStack = invSlots[i].ItemStack;

                if (slotStack.ItemInfo == itemStack.ItemInfo)
                {
                    // slotStack.Amount = Mathf.Abs(slotStack.ModifyAmount(-itemStack.Amount));
                }
            }

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
                }
                // Else if no slot selected...
                else if (gi.selectedSlot == null)
                {
                    // If slot stack has item...
                    if (slot.ItemStack.ItemInfo != null)
                    {
                        // Select slot
                        gi.selectedSlot = slot;
                    }
                }
                // Else, selected slot exists and is selecting different slot...
                else
                {
                    // If selecting weapon slot...
                    if (slot == gi.slotWeapon)
                    {
                        // If item being moved is a weapon...
                        if (gi.selectedSlot.ItemStack.ItemInfo is ItemInfoProjectile)
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
                    }
                }
            }
        }

        public static void HoverSlot(UIInventorySlot slot, bool ovrd = false)
        {
            if (slot != null & (ovrd | GameInfo.instance.parentInvUI.activeSelf))
            {
                GameInfo.instance.hoverSlot = slot;
            }
        }

        public static void SelectHotbar(int index)
        {
            GameInfo gi = GameInfo.instance;
            gi.selectedHotbar = gi.slotsHotbar[index];
        }

        public static SpaceObject SpawnSpaceObject(SpaceObjectInfo sos, Vector2 pos)
        {
            GameInfo gi = GameInfo.instance;
            SpaceObject r = null;
            bool pass = true;

            // If single instance type spawning...
            if (sos is SpaceObjectSpawnableInfo soss && soss.SpawnRateType == Enums.SpaceObjectSpawnRateType.SingleInstance)
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

        [System.Obsolete]
        public static void SpawnProjectile(ProjectileInfo projectile, Ship source)
        {
            // Projectile projectileObject = Instantiate(projectile.ProjectileObject, source.transform.position, source.transform.rotation);
            // Vector2 velocity = source.transform.up * projectile.Magnitude;
            // projectileObject.SpriteRenderer.sprite = projectile.;
            // projectileObject.Rigidbody.velocity = velocity;
            // projectileObject.ProjectileInfo = projectile;
            // projectileObject.SourceShip = source;

            // if (GameInfo.DEBUG_LOG)
            // {
            //     print("$[{projectile.Name}] Spawned from {source.name}");
            // }
        }

        public static void DestroySpaceObject(SpaceObject spaceObject)
        {
            GameInfo.instance.spaceObjects.Remove(spaceObject);
            Destroy(spaceObject.gameObject);
        }

        private IEnumerator RoutineSpawnSpaceObject(SpaceObjectSpawnableInfo soss)
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
        public void CallbackInput_Inventory(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => Util.ToggleActive(this.parentInvUI));
        public void CallbackInput_MenuDown(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.HoverSlot(this.hoverSlot.SlotDown));
        public void CallbackInput_MenuLeft(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.HoverSlot(this.hoverSlot.SlotLeft));
        public void CallbackInput_MenuRight(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.HoverSlot(this.hoverSlot.SlotRight));
        public void CallbackInput_MenuUp(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.HoverSlot(this.hoverSlot.SlotUp));
        public void CallbackInput_Rotate(InputAction.CallbackContext ctx)
        {
            this.rotationType = Enums.RotationType.RotateAxis;
            this.inputRotation = ctx.ReadValue<float>();
        }
        public void CallbackInput_SelectSlot(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () => GameInfo.SelectSlot(this.hoverSlot));
        public void CallbackInput_SlowDown(InputAction.CallbackContext ctx) => this.inputSlowDown = ctx.performed;
        public void CallbackInput_UseItem(InputAction.CallbackContext ctx) => OnButtonPress(ctx, () =>
        {
            ItemStack hotbarStack = this.selectedHotbar.ItemStack;
            ItemInfo hotbarItemInfo = hotbarStack.ItemInfo;

            if (hotbarItemInfo != null && !(hotbarItemInfo is ItemInfoProjectile))
            {
                hotbarItemInfo.Use(this.player);

                if (!hotbarItemInfo.Infinite)
                {
                    // TODO make sure this works properly
                    hotbarStack.ModifyAmount(-1);
                }
            }
        });

        private void OnButtonPress(InputAction.CallbackContext ctx, System.Action action) { if (ctx.performed) { action.Invoke(); } }
        #endregion
    }
}
