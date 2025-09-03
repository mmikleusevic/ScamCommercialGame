using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public event Action OnAllPlayersDead;
    
    [SerializeField] private PlayerController player;
    
    private readonly List<PlayerController> players = new List<PlayerController>();

    private void Start()
    {
        players.Add(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            PlayerController playerController = players.FirstOrDefault();

            if (!playerController)
            {
                OnAllPlayersDead?.Invoke();
                return;
            }
            
            Destroy(playerController.gameObject);
        }

        if (other.TryGetComponent(out Wall wall))
        {
            Destroy(wall.gameObject);
        }
    }

    public void AddPlayers(List<PlayerController> players)
    {
        players.AddRange(players);
    }

    public void KillPlayers(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (players.Count == 0)
            {
                OnAllPlayersDead?.Invoke();
                return;
            }

            PlayerController playerController = players[i];

            Destroy(players[i].gameObject);
            players.Remove(playerController);
        }
    }
}
