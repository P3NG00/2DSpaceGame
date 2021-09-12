using System.Collections;
using SpaceGame.Settings;
using SpaceGame.Ships;
using SpaceGame.SpaceObjects;
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
        [SerializeField] private Transform parentPlanet;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        public static GameModeSettings GMSettings => instance.gameMode;

        private static Vector2 PlayerPos => instance.player.transform.position;

        private void Start()
        {
            Application.targetFrameRate = framerate;

            foreach (SpaceObjectSettings sos in gameMode.SpaceObjects)
            {
                StartCoroutine(RoutineSpawnSpaceObject(sos));
            }
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

        private IEnumerator RoutineSpawnSpaceObject(SpaceObjectSettings sos)
        {
            while (true)
            {
                yield return new WaitForSeconds(sos.TimeBetweenChance);

                if (Random.value <= sos.ChanceSpawn)
                {
                    // // Instantiate space rock
                    Vector2 playerPos = player.transform.position;
                    Vector2 spawnOffset = Util.RandomUnitVector * sos.RandomSpawnDistance;
                    Vector2 spawnPos = playerPos + spawnOffset;
                    Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                    SpaceObject spaceObject = Instantiate(sos.RandomSpaceObject, spawnPos, spawnRot);

                    // // Create velocities
                    Vector2 velocity = Util.RandomUnitVector * sos.RandomVelocity;
                    float angularVelocity = Util.RandomUnit * sos.RandomAngularVelocity;
                    spaceObject.SetVelocities(velocity, angularVelocity);
                }
            }
        }

        // [System.Obsolete]
        // private IEnumerator RoutineCreateSpaceRocks()
        // {
        //     float waitTime, playerMagnitude;

        //     while (true)
        //     {
        //         waitTime = GMSettings.TimeBetweenSpaceRockChance;
        //         playerMagnitude = player.Rigidbody.velocity.magnitude * GMSettings.ScaleSpaceRockSpawnRate;

        //         TODO RE-IMPLEMENT SCALING WITH MOVEMENT
        //         if (playerMagnitude > 1f)
        //         {
        //             waitTime /= playerMagnitude;
        //         }

        //         yield return new WaitForSeconds(waitTime);

        //         if (Random.value <= GMSettings.ChanceSpaceRockSpawn)
        //         {
        //             SpawnSpaceRock();
        //         }
        //     }
        // }

        // [System.Obsolete]
        // private IEnumerator RoutineCleanDistantSpaceRocks()
        // {
        //     // Function to clean bodies
        //     void CleanBodies(Transform parent, float max)
        //     {
        //         foreach (Rigidbody2D rb in parent.GetComponentsInChildren<Rigidbody2D>())
        //         {
        //             if (Vector2.Distance(PlayerPos, rb.transform.position) > max)
        //             {
        //                 Destroy(rb.gameObject);
        //             }
        //         }
        //     }

        //     while (true)
        //     {
        //         // Wait for cleanup...
        //         yield return new WaitForSeconds(GMSettings.TimeBetweenSpaceRockCleanup);

        //         // Remove all distant Space Rocks
        //         CleanBodies(parentSpaceRock, GMSettings.DistanceSpaceRockMax);

        //         // Remove all distance Planets
        //         CleanBodies(parentPlanet, GMSettings.DistancePlanetMax);
        //     }
        // }

        // private void SpawnSpaceRock()
        // {
        //     // Instantiate space rock
        //     Vector2 spawnPos = PlayerPos + (Util.RandomUnitVector * Random.Range(GMSettings.DistanceSpaceRockSpawn, GMSettings.DistanceSpaceRockMax));
        //     Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        //     SpaceRock spaceRock = Instantiate(RandomSpaceRock, spawnPos, spawnRot, instance.parentSpaceRock);
        //     spaceRock.Scale = RandomSpaceRockScale;

        //     // Create velocities
        //     Vector2 velocity = Util.RandomUnitVector * Random.Range(GMSettings.MinSpaceRockVelocity, GMSettings.MaxSpaceRockVelocity);
        //     float angularVelocity = Util.RandomUnit * Random.Range(GMSettings.MinSpaceRockVelocityAngular, GMSettings.MaxSpaceRockVelocityAngular);
        //     spaceRock.SetVelocities(velocity, angularVelocity);
        // }
    }
}
