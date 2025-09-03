using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerZone playerZone;
    
    public void ResolvePlayers(PlayerController playerController, int numberOfPlayers)
    {
        List<PlayerController> players = new List<PlayerController>();
        int absNumberOfPlayers = Mathf.Abs(numberOfPlayers);
            
        if (numberOfPlayers > 0)
        {
            for (int i = 0; i < absNumberOfPlayers; i++)
            {
                PlayerController playerGO = Instantiate(playerController, playerController.transform.position, playerController.transform.rotation);
                players.Add(playerGO);
            }
            
            playerZone.AddPlayers(players);
        }
        else
        {
            playerZone.KillPlayers(absNumberOfPlayers);
        }
    }
}