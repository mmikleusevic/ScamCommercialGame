using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float maxTime;
    
    private Rigidbody rb;
    private IObjectPool<Bullet> pool;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = speed * transform.forward;   
    }

    private void OnEnable()
    {
        StartCoroutine(RemoveAfterTime());
    }

    public void Initialize(IObjectPool<Bullet> pool)
    {
        this.pool = pool;
    }

    private IEnumerator RemoveAfterTime()
    {
        yield return new WaitForSeconds(maxTime);
        
        pool.Release(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health enemyHealth))
        {
            enemyHealth.TakeDamage(damage);
            pool.Release(this);
        }

        if (other.TryGetComponent(out WallNumber wallNumber))
        {
            wallNumber.AddWallNumber();
        }
    }
}
