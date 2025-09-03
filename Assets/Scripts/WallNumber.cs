using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallNumber : MonoBehaviour
{
    private static bool hasTriggeredJustNow;
    
    [SerializeField] private WallUI wallUI;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private PlayerSpawner playerSpawner;
    private WallData wallData;
    
    private int wallNumber;

    private void Start()
    {
        playerSpawner = FindFirstObjectByType<PlayerSpawner>();
    }

    public void InitializeNumber(WallData wallData)
    {
        this.wallData = wallData;
        wallNumber = Random.Range(-50, 5);
        
        Color color = wallNumber < 0 ? this.wallData.badColor : this.wallData.goodColor;
        
        meshRenderer.material.color = color;
        wallUI.SetNumberText(wallNumber);
    }

    public void AddWallNumber()
    {
        wallNumber++;
        
        Color color = wallNumber < 0 ? wallData.badColor : wallData.goodColor;
        meshRenderer.material.color = color;
        
        SetWallNumber();
    }

    private void SetWallNumber()
    {
        wallUI.SetNumberText(wallNumber);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggeredJustNow) return;
        
        if (other.TryGetComponent(out PlayerController playerController))
        {
            hasTriggeredJustNow = true;

            StartCoroutine(TurnHasTriggeredToFalse());
            playerSpawner.ResolvePlayers(playerController, wallNumber);
            
            Destroy(gameObject);
        }
    }

    private IEnumerator TurnHasTriggeredToFalse()
    {
        yield return new WaitForSeconds(1f);
        
        hasTriggeredJustNow = false;
    }
}
