using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerControls controls;

    [Header("Movement Forces")]
    [SerializeField] private float thrustForce = 50f;
    [SerializeField] private float strafeForce = 30f;
    [SerializeField] private float verticalForce = 25f;

    [Header("Boost System")]
    [SerializeField] private float maxBoost = 100f;
    private float currentBoost = 100f;
    [SerializeField] private float boostRechargeRate = 30f;
    [SerializeField] private float boostDrainRate = 15f;

    [Header("Rotation")]
    [SerializeField] private float pitchSpeed = 100f;
    [SerializeField] private float yawSpeed = 100f;

    [Header("Speed Limits")]
    [SerializeField] private float maxNormalSpeed = 80f;
    [SerializeField] private float maxBoostSpeed = 120f;

    // Input values
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isAscending;
    private bool isDescending;
    private bool isPrimaryBoosting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();

        // Subscribe to input events
        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Gameplay.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Gameplay.Ascend.performed += ctx => isAscending = true;
        controls.Gameplay.Ascend.canceled += ctx => isAscending = false;

        controls.Gameplay.Descend.performed += ctx => isDescending = true;
        controls.Gameplay.Descend.canceled += ctx => isDescending = false;

        controls.Gameplay.PrimaryBoost.performed += ctx => isPrimaryBoosting = true;
        controls.Gameplay.PrimaryBoost.canceled += ctx => isPrimaryBoosting = false;
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Update()
    {
        HandleBoostRecharge();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        CapVelocity();
    }

    private void HandleMovement()
    {
        // Forward/backward thrust
        if (moveInput.y != 0)
        {
            rb.AddRelativeForce(Vector3.forward * moveInput.y * thrustForce);
        }

        // Strafe left/right
        if (moveInput.x != 0)
        {
            rb.AddRelativeForce(Vector3.right * moveInput.x * strafeForce);
        }

        // Vertical movement
        if (isAscending)
        {
            rb.AddRelativeForce(Vector3.up * verticalForce);
        }
        if (isDescending)
        {
            rb.AddRelativeForce(Vector3.down * verticalForce);
        }

        // Primary boost
        if (isPrimaryBoosting && currentBoost > 0)
        {
            rb.AddRelativeForce(Vector3.forward * thrustForce * 1.5f);
            currentBoost -= boostDrainRate * Time.fixedDeltaTime;
            currentBoost = Mathf.Max(0, currentBoost);
        }
    }

    private void HandleRotation()
    {
        // Pitch (look up/down)
        if (lookInput.y != 0)
        {
            float pitch = -lookInput.y * pitchSpeed * Time.fixedDeltaTime;
            rb.transform.Rotate(Vector3.right, pitch, Space.Self);
        }

        // Yaw (look left/right)
        if (lookInput.x != 0)
        {
            float yaw = lookInput.x * yawSpeed * Time.fixedDeltaTime;
            rb.transform.Rotate(Vector3.up, yaw, Space.Self);
        }
    }

    private void CapVelocity()
    {
        float maxSpeed = isPrimaryBoosting ? maxBoostSpeed : maxNormalSpeed;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void HandleBoostRecharge()
    {
        if (!isPrimaryBoosting && currentBoost < maxBoost)
        {
            currentBoost += boostRechargeRate * Time.deltaTime;
            currentBoost = Mathf.Min(currentBoost, maxBoost);
        }
    }

    private void OnDrawGizmos()
    {
        if (rb == null) return;

        // Draw velocity vector in green
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, rb.linearVelocity);

        // Draw forward direction in blue
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }
}