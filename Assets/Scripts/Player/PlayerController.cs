using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;

    [Header("Input Values")]
    private Vector2 moveInput;

    [Header("Movement Settings")]
    public float moveSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Player Controller Started!");
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Create a movement direction based on input
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // Apply force to move the player
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}