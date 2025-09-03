using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private PlayerZone playerZone;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    private void OnEnable()
    {
        enemySpawner.OnAllEnemiesDied += Win;
        playerZone.OnAllPlayersDead += Lose;
    }

    private void OnDisable()
    {
        enemySpawner.OnAllEnemiesDied -= Win;
        playerZone.OnAllPlayersDead -= Lose;
    }

    private void Win()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
        endText.text = "You win!";
    }

    private void Lose()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
        endText.text = "You lose!";
    }

    private void AddScore(Health enemyHealth)
    {
        score += enemyHealth.GetScoreAmount();
        scoreText.text = "Score: " + score;
    }

    public void SubscribeToEnemy(Health enemyHealth)
    {
        enemyHealth.OnDeath += AddScore;
    }
    
    public void UnsubscribeFromEnemy(Health enemyHealth)
    {
        enemyHealth.OnDeath -= AddScore;
    }
}
