using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Health> OnDeath;

    [SerializeField] private EnemyData enemyData;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        int newHealth = Mathf.Clamp(currentHealth - amount, 0, enemyData.maxHealth);
        SetHealth(newHealth);
    }

    private void SetHealth(int health)
    {
        currentHealth = health;

        if (currentHealth > 0) return;
        
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public int GetScoreAmount()
    {
        return enemyData.score;
    }
}