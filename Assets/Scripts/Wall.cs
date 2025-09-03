using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wall : MonoBehaviour
{
    [SerializeField] private WallData wallData;

    private WallNumber wallNumber;

    private void Awake()
    {
        wallNumber = GetComponent<WallNumber>();
    }

    private void Start()
    {
        Initialize();
    }
    
    private void Initialize()
    {
        wallNumber.InitializeNumber(wallData);
    }
}