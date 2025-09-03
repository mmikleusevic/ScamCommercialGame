using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Transform wallParent;
    [SerializeField] private Wall wallPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int minSpawnTime;
    [SerializeField] private int maxSpawnTime;
    
    private Coroutine spawnCoroutine;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        
        spawnCoroutine = StartCoroutine(SpawnWall());
    }
    
    private void OnEnable()
    {
        enemySpawner.OnEnemiesStoppedSpawning += StopSpawning;
    }

    private void OnDisable()
    {
        enemySpawner.OnEnemiesStoppedSpawning -= StopSpawning;
    }

    private IEnumerator SpawnWall()
    {
        foreach (Transform spawnTransform in spawnPoints)
        {
            Wall wall = Instantiate(wallPrefab, spawnTransform.position, Quaternion.identity);
            wall.transform.SetParent(wallParent);
            wall.transform.position = spawnTransform.position;
        }
        
        float timeToWait = Random.Range(minSpawnTime, maxSpawnTime);

        yield return new WaitForSeconds(timeToWait);

        yield return SpawnWall();
    }

    private void StopSpawning()
    {
        StopCoroutine(spawnCoroutine);
    }
}
