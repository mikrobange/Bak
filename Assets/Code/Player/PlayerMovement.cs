using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private PlayerInputHandler inputHandler;
    private float horizontalInput = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = PlayerInputHandler.Instance;
    }
    private void Update()
    {
        horizontalInput = inputHandler.MoveInput.x;
        if (horizontalInput != 0)
        {
            FlipSprite(horizontalInput);
        }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }
    void ApplyMovement()
    {
        float speed = moveSpeed;
        rb.velocity = inputHandler.MoveInput * speed;
    }
    private void FlipSprite(float horizontalMovement)
    {

        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
