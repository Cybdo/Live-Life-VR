using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using OVRSimpleJSON;
using System.IO;
using System;




public class PlayerTick : MonoBehaviour
{
    public bool overrideSpawn;
    public int overrideSpawnPoint;
    void Start()
    {

        try
        {
            string json = @"
            [
	            {
		            ""name""	: ""cabin"",
	            	""x""		: 975,
	            	""y""		: 125,
	            	""z""		: 480
	            },
	            {
	            	""name""	: ""lighthouse"",
            		""x""		: 44,
            		""y""		: 135,
            		""z""		: 940
            	},
                {
                    ""name""	: ""roofless shed"",
            		""x""		: 482,
            		""y""		: 145,
            		""z""		: 423
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

        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while reading the file: {ex.Message}");
        }
    }

    void Update()
    {
        // Update logic here
    }
}
