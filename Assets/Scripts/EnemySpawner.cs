using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action OnEnemiesStoppedSpawning;
    public event Action OnAllEnemiesDied;
    
    [SerializeField] private Health[] enemies;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform[] spawnPositions;
    
    [SerializeField] private float minSpawnWaitTime;
    [SerializeField] private float maxSpawnWaitTime;
    [SerializeField] private int minNumberOfEnemies;
    [SerializeField] private int maxNumberOfEnemies;
    [SerializeField] private int maxNumberOfCycles;
    
    private Coroutine spawnCoroutine;

    private int numberOfEnemies;
    private bool enemiesStoppedSpawning;
    
    private IEnumerator Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRandomly());
        
        yield return new WaitForSeconds(60f);
        
        StopCoroutine(spawnCoroutine);
        enemiesStoppedSpawning = true;
        OnEnemiesStoppedSpawning?.Invoke();
    }

    private IEnumerator SpawnRandomly()
    {
        float randomSpawnTime = Random.Range(minSpawnWaitTime, maxSpawnWaitTime);
        float randomNumberOfEnemiesToSpawn = Random.Range(minNumberOfEnemies, maxNumberOfEnemies);

        for (int i = 0; i < randomNumberOfEnemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            Health enemyPrefab = enemies[randomIndex];
            Health enemy = Instantiate(enemies[randomIndex], transform.position, enemyPrefab.transform.rotation);
            enemy.transform.SetParent(enemyParent);
            enemy.transform.position = spawnPositions[i].position;
            
            GameManager.Instance.SubscribeToEnemy(enemy);
            enemy.OnDeath += RemoveEnemy;
            numberOfEnemies++;
        }
        
        yield return new WaitForSeconds(randomSpawnTime);
        
        yield return SpawnRandomly();
    }

    private void RemoveEnemy(Health enemyHealth)
    {
        numberOfEnemies--;

        GameManager.Instance.UnsubscribeFromEnemy(enemyHealth);
        enemyHealth.OnDeath -= RemoveEnemy;

        if (numberOfEnemies <= 0 && enemiesStoppedSpawning) OnAllEnemiesDied?.Invoke();
    }
}
