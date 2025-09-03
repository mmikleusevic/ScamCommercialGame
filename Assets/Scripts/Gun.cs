using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate;
    
    private ObjectPool<Bullet> bulletPool;

    private Transform bulletPoolParent;
    private Coroutine shootCoroutine;
    
    private void Awake()
    {
        bulletPoolParent = new GameObject("BulletPool").transform;

        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 100,
            maxSize: 200
        );
    }

    private void Start()
    {
        shootCoroutine = StartCoroutine(Shoot());
    }

    private void OnEnable()
    {
        enemySpawner.OnAllEnemiesDied += StopShooting;
    }

    private void OnDisable()
    {
        enemySpawner.OnAllEnemiesDied -= StopShooting;
    }

    private IEnumerator Shoot()
    {
        Bullet bullet = bulletPool.Get();
        bullet.Initialize(bulletPool);
        bullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        
        yield return new WaitForSeconds(fireRate);

        yield return Shoot();
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation, bulletPoolParent);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void StopShooting()
    {
        StopCoroutine(shootCoroutine); 
    }
}