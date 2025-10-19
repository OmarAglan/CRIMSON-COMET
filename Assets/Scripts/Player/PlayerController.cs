using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Transform cameraTransform;
    private CameraFollow cameraFollow;

    [Header("Input Values")]
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isAscending;
    private bool isDescending;

    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float verticalSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (rb == null)
            Debug.LogError("Rigidbody not found!");
        if (cameraTransform == null)
            Debug.LogError("Main Camera not found!");
        if (cameraFollow == null)
            Debug.LogError("CameraFollow script not found on Main Camera!");

        Debug.Log("Player Controller Started!");
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        if (moveInput.magnitude > 0.1f)
            Debug.Log("Applying movement force!");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Force);

        float verticalInput = 0f;
        if (isAscending) verticalInput += 1f;
        if (isDescending) verticalInput -= 1f;

        Vector3 verticalDirection = new Vector3(0, verticalInput, 0);
        rb.AddForce(verticalDirection * verticalSpeed, ForceMode.Force);
    }

    void HandleRotation()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 movementDirection = rb.linearVelocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        if (cameraFollow != null)
        {
            cameraFollow.ReceiveLookInput(lookInput);
        }
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