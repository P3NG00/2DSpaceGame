using System.Text;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project
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

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("Shop", order = 2)]
        [SerializeField] private ShopItem[] shopItems;

        [Header("Info", order = 3)]
        [SerializeField, Min(0f)] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField, Min(0f)] private float minAngularVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private float distanceSpaceRockSpawn;
        [SerializeField] private float maxDistanceSpaceRock;
        [SerializeField] private float timeBetweenSpaceRockCleanup;
        [SerializeField] private float scaleSpawnSpaceRocksWithSpeed;
        [SerializeField, Min(0f)] private float timeBetweenSpaceRockChance;
        [SerializeField, Range(0f, 1f)] private float chanceSpaceRockSpawn;
        [SerializeField, Min(0f)] private float minSpaceRockScale;
        [SerializeField] private float maxSpaceRockScale;

        private void ValidateMinMax(float min, ref float max)
        {
            if (min > max)
            {
                max = min;
            }
        }

        private void OnValidate()
        {
            ValidateMinMax(minVelocity, ref maxVelocity);
            ValidateMinMax(minAngularVelocity, ref maxAngularVelocity);
            ValidateMinMax(minSpaceRockScale, ref maxSpaceRockScale);
        }

        [Header("References", order = 99)]
        [SerializeField] private SpaceRock[] prefabSpaceRocks;
        [SerializeField] private Player player;
        [SerializeField] private Transform parentSpaceRock;
        [SerializeField] private TMP_Text textCredits;

        public static float MinSpaceRockScale => instance.minSpaceRockScale;

        private static float RandomUnit => Random.Range(-1f, 1f);
        private static float RandomSpaceRockScale => Random.Range(instance.minSpaceRockScale, instance.maxSpaceRockScale);
        private static Vector2 RandomUnitVector => new Vector2(RandomUnit, RandomUnit).normalized;
        private static Vector2 PlayerPos => instance.player.transform.position;
        private static SpaceRock RandomSpaceRock => instance.prefabSpaceRocks[Random.Range(0, instance.prefabSpaceRocks.Length)];

        private int indexShop = 0;

        private void Start()
        {
            Application.targetFrameRate = framerate;
            StartCoroutine(RoutineCreateSpaceRocks());
            StartCoroutine(RoutineCleanDistantSpaceRocks());
        }

        private void Update()
        {
            textCredits.text = credits.ToString();
        }

        private IEnumerator RoutineCreateSpaceRocks()
        {
            float waitTime, playerMagnitude;

            while (true)
            {
                waitTime = timeBetweenSpaceRockChance;
                playerMagnitude = player.Rigidbody.velocity.magnitude * scaleSpawnSpaceRocksWithSpeed;

                if (playerMagnitude > 1f)
                {
                    waitTime /= playerMagnitude;
                }

                yield return new WaitForSeconds(waitTime);

                if (Random.value <= chanceSpaceRockSpawn)
                {
                    SpawnSpaceRock();
                }
            }
        }

        private IEnumerator RoutineCleanDistantSpaceRocks()
        {
            while (true)
            {
                // Wait for cleanup...
                yield return new WaitForSeconds(timeBetweenSpaceRockCleanup);

                // Remove all distant Space Rocks
                Rigidbody2D[] spaceRocks = parentSpaceRock.GetComponentsInChildren<Rigidbody2D>();

                foreach (Rigidbody2D sr in spaceRocks)
                {
                    if (Vector2.Distance(PlayerPos, sr.transform.position) > maxDistanceSpaceRock)
                    {
                        Destroy(sr.gameObject);
                    }
                }
            }
        }

        public static void SpawnSpaceRock()
        {
            // Instantiate space rock
            Vector2 spawnPos = PlayerPos + (RandomUnitVector * instance.distanceSpaceRockSpawn);
            Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            SpaceRock spaceRock = Instantiate(RandomSpaceRock, spawnPos, spawnRot, instance.parentSpaceRock);
            spaceRock.Scale = RandomSpaceRockScale;

            // Create velocities
            Vector2 velocity = RandomUnitVector * Random.Range(instance.minVelocity, instance.maxVelocity);
            float angularVelocity = RandomUnit * Random.Range(instance.minAngularVelocity, instance.maxAngularVelocity);
            spaceRock.SetVelocities(velocity, angularVelocity);
        }

        public static void IncrementCredit() => ++instance.credits;

        public void ShopNavigation(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            // TODO shop nav
            // x = left,right
            // y = up,down
        }

        [System.Serializable]
        public class ShopItem
        {
            [SerializeField] private string itemName;
            [SerializeField] private string itemDescriptor;
            [SerializeField] private int costCredits;

            public override string ToString()
            {
                return $"{itemName}\n" +
                    $"{itemDescriptor}\n" +
                    $"{costCredits}";
            }
        }
    }
}
