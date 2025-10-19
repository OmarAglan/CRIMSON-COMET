using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Transform cameraTransform;

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
        cameraTransform = Camera.main.transform;
        Debug.Log("Player Controller Started!");
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Get camera's forward and right directions (ignoring vertical tilt)
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Flatten the directions (remove Y component) for horizontal movement
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction relative to camera
        Vector3 moveDirection = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        // Vertical movement (up and down) - stays world-relative
        float verticalInput = 0f;
        if (isAscending) verticalInput += 1f;
        if (isDescending) verticalInput -= 1f;

        Vector3 verticalDirection = new Vector3(0, verticalInput, 0);
        rb.AddForce(verticalDirection * verticalSpeed, ForceMode.Force);
    }
    void HandleRotation()
    {
        // Only rotate if we're moving
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            // Get the direction we're moving
            Vector3 movementDirection = rb.linearVelocity.normalized;

            // Create a rotation that faces that direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Smoothly rotate towards that direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        }
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