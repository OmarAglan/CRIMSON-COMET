using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Distance")]
    public float distance = 10f;
    public float minDistance = 5f;
    public float maxDistance = 20f;

    [Header("Camera Rotation")]
    public float mouseSensitivity = 2f;
    public float gamepadSensitivity = 100f;
    public float rotationSmoothSpeed = 10f;

    [Header("Camera Limits")]
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    // Input values
    private Vector2 lookInput;

    // Current rotation angles
    private float currentYaw = 0f;   // Horizontal rotation
    private float currentPitch = 20f; // Vertical rotation

    void Start()
    {
        // Hide and lock the cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleCameraRotation();
        UpdateCameraPosition();
    }

    void HandleCameraRotation()
    {
        // Get look input and apply sensitivity
        float lookX = lookInput.x;
        float lookY = lookInput.y;

        // Different sensitivity for mouse vs gamepad
        // Mouse gives delta movement, gamepad gives continuous values
        if (Mathf.Abs(lookInput.x) > 0 || Mathf.Abs(lookInput.y) > 0)
        {
            // Check if input is from mouse (usually larger values) or gamepad
            bool isMouseInput = Mathf.Abs(lookInput.x) > 2f || Mathf.Abs(lookInput.y) > 2f;

            if (isMouseInput)
            {
                currentYaw += lookX * mouseSensitivity * 0.1f;
                currentPitch -= lookY * mouseSensitivity * 0.1f;
            }
            else
            {
                currentYaw += lookX * gamepadSensitivity * Time.deltaTime;
                currentPitch -= lookY * gamepadSensitivity * Time.deltaTime;
            }
        }

        // Clamp vertical rotation
        currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);
    }

    void UpdateCameraPosition()
    {
        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

        // Calculate position based on rotation and distance
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = target.position + offset;

        // Apply position
        transform.position = targetPosition;

        // Look at target
        transform.LookAt(target.position);
    }

    // Called by Input System
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    // Allow player to unlock cursor with ESC key
    void Update()
    {
        // Use new Input System to detect ESC key
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}