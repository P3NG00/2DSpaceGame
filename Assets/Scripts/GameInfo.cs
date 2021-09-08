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
        [Header("Info", order = 0)]
        [SerializeField] private float maxVelocity;
        [SerializeField] private float maxAngularVelocity;
        [SerializeField] private float distanceSpaceRockSpawn;

        [Header("References", order = 99)]
        [SerializeField] private Rigidbody2D[] prefabSpaceRocks;

        private float RandomUnit => Random.Range(-1f, 1f);
        private Vector2 RandomUnitVector => new Vector2(RandomUnit, RandomUnit).normalized;
        private Rigidbody2D RandomSpaceRock => prefabSpaceRocks[Random.Range(0, prefabSpaceRocks.Length)];

        private void SpawnSpaceRock()
        {
            // Instantiate space rock
            Vector2 spawnPos = (Vector2)transform.position + RandomUnitVector * distanceSpaceRockSpawn;
            Rigidbody2D spaceRock = Instantiate(RandomSpaceRock, spawnPos, Quaternion.identity);

            // Give velocity
            spaceRock.velocity = RandomUnitVector * Random.Range(0f, maxVelocity);
            spaceRock.angularVelocity = RandomUnit * Random.Range(0f, maxAngularVelocity);
        }

        // TODO make method to spawn space rocks
    }
}
