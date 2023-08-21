using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnDelay = 2f;
    public float spawnRange = 3f;

    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to spawn a new platform
        if (Time.time - lastSpawnTime >= spawnDelay)
        {
            // Spawn a new platform
            GameObject platform = Instantiate(platformPrefab);

            // Set the position of the new platform randomly within a range
            float xPosition = Random.Range(-spawnRange, spawnRange);
            platform.transform.position = transform.position + new Vector3(xPosition, 0, 0);

            // Reset the last spawn time
            lastSpawnTime = Time.time;
        }
    }
}