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
        [SerializeField] private GameModeSettings settings;

        [Header("Init", order = 1)]
        [SerializeField] private int framerate;

        [Header("References", order = 99)]
        [SerializeField] private ShipPlayer player;
        [SerializeField] private Transform parentSpaceObjects;
        [SerializeField] private TMP_Text textCredits;
        [SerializeField] private TMP_Text prefabTextCreditPopup;

        public static GameModeSettings GMSettings => instance.settings;

        private SpaceObject[] SpaceObjects => parentSpaceObjects.GetComponentsInChildren<SpaceObject>();

        private void Start()
        {
            foreach (SpaceObjectSettings sos in settings.SpaceObjectsToSpawn)
            {
                StartCoroutine(RoutineSpawnSpaceObject(sos));
            }

            StartCoroutine(RoutineCleanDistantSpaceObjects());
            Application.targetFrameRate = framerate;
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
            float waitTime, magnitude;

            while (true)
            {
                // Default wait time
                waitTime = sos.TimeBetweenChance;

                // If scale wait time...
                if (sos.SpawnType == SpaceObjectSpawnType.ScaleWithMagnitude)
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
                    // Setup variable for checks
                    bool pass = true;

                    // If single instance type spawning...
                    if (sos.SpawnType == SpaceObjectSpawnType.SingleInstance)
                    {
                        // Search through all space objects and see if it's already instantiated
                        foreach (SpaceObject so in SpaceObjects)
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
                        // Instantiate space rock
                        Vector2 spawnOffset = player.transform.up * sos.RandomSpawnDistance;
                        spawnOffset += (Vector2)player.transform.right * sos.RandomSpawnWidth;
                        Vector2 spawnPos = (Vector2)player.transform.position + spawnOffset;
                        Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                        SpaceObject spaceObject = Instantiate(sos.RandomSpaceObject, spawnPos, spawnRot, parentSpaceObjects);
                        spaceObject.Scale = sos.RandomScale;
                        Vector2 velocity = Util.RandomUnitVector * sos.RandomVelocity;
                        float angularVelocity = Random.Range(-1f, 1f) * sos.RandomAngularVelocity;
                        spaceObject.Rigidbody.velocity = velocity;
                        spaceObject.Rigidbody.angularVelocity = angularVelocity;
                    }
                }
            }
        }

        private IEnumerator RoutineCleanDistantSpaceObjects()
        {
            while (true)
            {
                yield return new WaitForSeconds(settings.TimeBetweenCleanup);

                foreach (SpaceObject so in SpaceObjects)
                {
                    if (Vector2.Distance(player.transform.position, so.transform.position) > so.Settings.DistanceMax)
                    {
                        Destroy(so.gameObject);
                    }
                }
            }
        }
    }
}
