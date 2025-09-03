using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    
    private Rigidbody rb;
    
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movement = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }
        
        float xPosition = Mathf.Clamp(transform.position.x, minX, maxX);
        
        transform.position = new Vector3(xPosition, 0, 0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = movement * speed;
    }
}
