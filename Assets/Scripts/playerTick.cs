using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class PlayerTick : MonoBehaviour
{
    public bool overrideSpawn;
    public int overrideSpawnPoint;
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public int enemyCount = 10; // Number of enemies to maintain
    public float spawnProximity = 50.0f; // Proximity within which to spawn enemies
    public int enemySpawnDelay;
    private List<GameObject> enemies = new List<GameObject>(); // List to keep track of spawned enemies
    private bool Maintain = false; // Flag to maintain enemy count


    void Start()
    {
        try
        {
            string json = @"
            [
                {
                    ""name""    : ""cabin"",
                    ""x""        : 975,
                    ""y""        : 125,
                    ""z""        : 480
                },
                {
                    ""name""    : ""lighthouse"",
                    ""x""        : 44,
                    ""y""        : 135,
                    ""z""        : 940
                },
                {
                    ""name""    : ""roofless shed"",
                    ""x""        : 482,
                    ""y""        : 145,
                    ""z""        : 423
                },
                {
                    ""name""     : ""bridge"",
                    ""x""        : 877,
                    ""y""        : 148,
                    ""z""        : 379
                },
                {
                    ""name""     : ""plane"",
                    ""x""        : 383,
                    ""y""        : 161,
                    ""z""        : 645
                }
            ]";

            List<SpawnCoordsParser> spawnCoords = JsonConvert.DeserializeObject<List<SpawnCoordsParser>>(json);

            if (spawnCoords == null || spawnCoords.Count == 0)
            {
                Debug.LogError("No spawn coordinates found or JSON deserialization failed.");
                return;
            }

            int[] x_coords = new int[spawnCoords.Count];
            int[] y_coords = new int[spawnCoords.Count];
            int[] z_coords = new int[spawnCoords.Count];

            int spawnpointIndex;

            for (int i = 0; i < spawnCoords.Count; i++)
            {
                x_coords[i] = spawnCoords[i].X;
                y_coords[i] = spawnCoords[i].Y;
                z_coords[i] = spawnCoords[i].Z;

                // Debug output to verify coordinates are being loaded correctly
                Debug.Log($"Loaded spawn point {i}: ({x_coords[i]}, {y_coords[i]}, {z_coords[i]})");
            }

            if (!overrideSpawn)
            {
                spawnpointIndex = UnityEngine.Random.Range(0, x_coords.Length);
            }
            else
            {
                spawnpointIndex = overrideSpawnPoint;
            }

            // Debug output to verify random index
            Debug.Log($"Random spawn point index: {spawnpointIndex}");
            // Move the GameObject to the selected random position
            transform.position = new Vector3(x_coords[spawnpointIndex], y_coords[spawnpointIndex], z_coords[spawnpointIndex]);

            // Debug output to verify the position is being set correctly
            Debug.Log($"Spawned at: ({transform.position.x}, {transform.position.y}, {transform.position.z})");

            // Start the coroutine to wait and then spawn enemies
            StartCoroutine(SpawnEnemiesAfterDelay(enemySpawnDelay)); // 180 seconds = 3 minutes
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while reading the file: {ex.Message}");
        }
    }

    void Update()
    {
        // Continuously ensure the number of enemies
        MaintainEnemyCount();
    }

    IEnumerator SpawnEnemiesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Maintain = true;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemies.Add(newEnemy);
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * spawnProximity;
        randomDirection.y = 2; // Keep the spawn position on the same plane
        return transform.position + randomDirection;
    }

    void MaintainEnemyCount()
    {
        if (Maintain)
        {
            // Remove destroyed enemies from the list
            enemies.RemoveAll(enemy => enemy == null);

            // Spawn new enemies if the count is less than the desired number
            while (enemies.Count < enemyCount)
            {
                SpawnEnemy();
            }
        }
    }
}