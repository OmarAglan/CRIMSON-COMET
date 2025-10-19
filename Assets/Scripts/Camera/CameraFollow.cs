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

    private Vector2 lookInput;
    private float currentYaw = 0f;
    private float currentPitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
            else
                Debug.LogError("No target assigned and no Player tag found!");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        HandleCameraRotation();
        UpdateCameraPosition();
    }

    void HandleCameraRotation()
    {
        float lookX = lookInput.x;
        float lookY = lookInput.y;

        if (Mathf.Abs(lookInput.x) > 0 || Mathf.Abs(lookInput.y) > 0)
        {
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

        currentPitch = Mathf.Clamp(currentPitch, minVerticalAngle, maxVerticalAngle);
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = target.position + offset;

        transform.position = targetPosition;
        transform.LookAt(target.position);
    }

    public void ReceiveLookInput(Vector2 input)
    {
        lookInput = input;
    }

    void Update()
    {
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