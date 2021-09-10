using System.Collections;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.Utilities;
using TMPro;
using UnityEngine;

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
        [SerializeField] private GameModeSettings gameMode;

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("References", order = 99)]
        [SerializeField] private SpaceRock[] prefabSpaceRocks;
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceRock;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        public static GameModeSettings GMSettings => instance.gameMode;

        private static float RandomSpaceRockScale => Random.Range(GMSettings.MinSpaceRockScale, GMSettings.MaxSpaceRockScale);
        private static Vector2 PlayerPos => instance.player.transform.position;
        private static SpaceRock RandomSpaceRock => instance.prefabSpaceRocks[Random.Range(0, instance.prefabSpaceRocks.Length)];

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

        public static void GiveCredits(int amount, Vector2 position)
        {
            instance.credits += amount;

            TMP_Text textPopup = Instantiate(instance.prefabTextCreditPopup, position, Quaternion.identity);
            textPopup.text = $"+{amount}";
            Destroy(textPopup.gameObject, 0.5f);
        }

        private IEnumerator RoutineCreateSpaceRocks()
        {
            float waitTime, playerMagnitude;

            while (true)
            {
                waitTime = GMSettings.TimeBetweenSpaceRockChance;
                playerMagnitude = player.Rigidbody.velocity.magnitude * GMSettings.ScaleSpaceRockSpawnRate;

                if (playerMagnitude > 1f)
                {
                    waitTime /= playerMagnitude;
                }

                yield return new WaitForSeconds(waitTime);

                if (Random.value <= GMSettings.ChanceSpaceRockSpawn)
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
                yield return new WaitForSeconds(GMSettings.TimeBetweenSpaceRockCleanup);

                // Remove all distant Space Rocks
                Rigidbody2D[] spaceRocks = parentSpaceRock.GetComponentsInChildren<Rigidbody2D>();

                foreach (Rigidbody2D sr in spaceRocks)
                {
                    if (Vector2.Distance(PlayerPos, sr.transform.position) > GMSettings.DistanceSpaceRockMax)
                    {
                        Destroy(sr.gameObject);
                    }
                }
            }
        }

        public static void SpawnSpaceRock()
        {
            // Instantiate space rock
            Vector2 spawnPos = PlayerPos + (Util.RandomUnitVector * Random.Range(GMSettings.DistanceSpaceRockSpawn, GMSettings.DistanceSpaceRockMax));
            Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            SpaceRock spaceRock = Instantiate(RandomSpaceRock, spawnPos, spawnRot, instance.parentSpaceRock);
            spaceRock.Scale = RandomSpaceRockScale;

            // Create velocities
            Vector2 velocity = Util.RandomUnitVector * Random.Range(GMSettings.MinSpaceRockVelocity, GMSettings.MaxSpaceRockVelocity);
            float angularVelocity = Util.RandomUnit * Random.Range(GMSettings.MinSpaceRockVelocityAngular, GMSettings.MaxSpaceRockVelocityAngular);
            spaceRock.SetVelocities(velocity, angularVelocity);
        }
    }
}
