using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;

    [Header("Input Values")]
    private Vector2 moveInput;
    private bool isAscending;
    private bool isDescending;

    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float verticalSpeed = 10f;

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
        // Horizontal movement (forward, backward, left, right)
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        // Vertical movement (up and down)
        float verticalInput = 0f;
        if (isAscending) verticalInput += 1f;
        if (isDescending) verticalInput -= 1f;

        Vector3 verticalDirection = new Vector3(0, verticalInput, 0);
        rb.AddForce(verticalDirection * verticalSpeed, ForceMode.Force);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAscend(InputAction.CallbackContext context)
    {
        if (context.performed)
            isAscending = true;
        else if (context.canceled)
            isAscending = false;
    }

    public void OnDescend(InputAction.CallbackContext context)
    {
        if (context.performed)
            isDescending = true;
        else if (context.canceled)
            isDescending = false;
    }
}