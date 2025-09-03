using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += Vector3.back * (speed * Time.deltaTime);
    }
}