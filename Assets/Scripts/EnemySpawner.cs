using System;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform quadraticSpawn;
    public Transform cubicSpawn;
    public Transform targetLocation;

    public float quadraticSpawnInterval = 2f;
    public float cubicSpawnInterval = 3f;

    public float minMoveSpeed = 1f;  // Minimum movement speed
    public float maxMoveSpeed = 3f;  // Maximum movement speed

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnQuadraticEnemies());
        StartCoroutine(SpawnCubicEnemies());
    }

    private void Update()
    {
         if(GameManager.Instance.playerHP <= 0) StopSpawning(); 
    }

    IEnumerator SpawnQuadraticEnemies()
    {
        while (isSpawning)
        {
            SpawnEnemy(quadraticSpawn, MovementType.Quadratic);
            yield return new WaitForSeconds(quadraticSpawnInterval);
        }
    }

    IEnumerator SpawnCubicEnemies()
    {
        while (isSpawning)
        {
            SpawnEnemy(cubicSpawn, MovementType.Cubic);
            yield return new WaitForSeconds(cubicSpawnInterval);
        }
    }

    void SpawnEnemy(Transform spawnPoint, MovementType moveType)
    {
        if (!isSpawning) return; // Prevent new spawns if stopped

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

        if (movement != null)
        {
            // Set random speed for the enemy between min and max
            float moveSpeed = UnityEngine.Random.Range(minMoveSpeed, maxMoveSpeed);
            movement.Initialize(targetLocation, moveType, moveSpeed);  // Pass the speed to Initialize method
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines(); // Ensures coroutines stop running immediately
    }
}