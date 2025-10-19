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
    public float moveSpeed = 15f;
    public float verticalSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Check if components are found
        if (rb == null)
            Debug.LogError("Rigidbody not found!");
        if (cameraTransform == null)
            Debug.LogError("Main Camera not found!");

        Debug.Log("Player Controller Started!");
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Debug the movement
        if (moveInput.magnitude > 0.1f)
            Debug.Log("Applying movement force!");

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
        Debug.Log("Move Input: " + moveInput);
    }

    public void OnAscend(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAscending = true;
            Debug.Log("Ascending!");
        }
        else if (context.canceled)
        {
            isAscending = false;
            Debug.Log("Stopped Ascending");
        }
    }

    public void OnDescend(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDescending = true;
            Debug.Log("Descending!");
        }
        else if (context.canceled)
        {
            isDescending = false;
            Debug.Log("Stopped Descending");
        }
    }
}