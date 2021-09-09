using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [Header("Init", order = 0)]
        [SerializeField] private int framerate;

        [Header("Info", order = 1)]
        [SerializeField] private float minVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float minAngularVelocity;
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
        [SerializeField] private Rigidbody2D[] prefabSpaceRocks;
        [SerializeField] private Player player;

        private float RandomUnit => Random.Range(-1f, 1f);
        private float RandomSpaceRockScale => Random.Range(minSpaceRockScale, maxSpaceRockScale);
        private Vector2 RandomUnitVector => new Vector2(RandomUnit, RandomUnit).normalized;
        private Vector2 PlayerPos => player.transform.position;
        private Rigidbody2D RandomSpaceRock => prefabSpaceRocks[Random.Range(0, prefabSpaceRocks.Length)];

        private List<Rigidbody2D> spaceRocks = new List<Rigidbody2D>();

        private void Start()
        {
            Application.targetFrameRate = framerate;
            StartCoroutine(RoutineCreateSpaceRocks());
            StartCoroutine(RoutineCleanDistantSpaceRocks());
        }

        private IEnumerator RoutineCreateSpaceRocks()
        {
            float waitTime, playerMagnitude;

            while (true)
            {
                waitTime = timeBetweenSpaceRockChance;
                playerMagnitude = player.Rigidbody.velocity.magnitude * scaleSpawnSpaceRocksWithSpeed;

                if (playerMagnitude != 0)
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
            List<Rigidbody2D> spaceRocksToRemove = new List<Rigidbody2D>();

            while (true)
            {
                // Wait for cleanup...
                yield return new WaitForSeconds(timeBetweenSpaceRockCleanup);

                // Add each Space Rock too far away from the player to a removal list
                spaceRocks.ForEach(sr =>
                {
                    if (Vector2.Distance(PlayerPos, sr.transform.position) > maxDistanceSpaceRock)
                    {
                        spaceRocksToRemove.Add(sr);
                    }
                });

                // Destroy all Space Rocks on removal list
                spaceRocksToRemove.ForEach(sr =>
                {
                    spaceRocks.Remove(sr);
                    Destroy(sr.gameObject);
                });

                // Clear removal list
                spaceRocksToRemove.Clear();
            }
        }

        private void SpawnSpaceRock()
        {
            // Instantiate space rock
            Vector2 spawnPos = PlayerPos + (RandomUnitVector * distanceSpaceRockSpawn);
            Quaternion spawnRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            Rigidbody2D spaceRock = Instantiate(RandomSpaceRock, spawnPos, Quaternion.identity);

            // Change scale
            spaceRock.transform.localScale = Vector2.one * RandomSpaceRockScale;

            // Give velocity
            spaceRock.velocity = RandomUnitVector * Random.Range(minVelocity, maxVelocity);
            spaceRock.angularVelocity = RandomUnit * Random.Range(minAngularVelocity, maxAngularVelocity);

            // Add to list
            spaceRocks.Add(spaceRock);
        }
    }
}
