// ============================================================================
// CRIMSON COMET - Player Controller
// ============================================================================
// Handles all player movement, rotation, and boost mechanics in 6DoF space.
// Uses Unity's new Input System for controller support.
// 
// Features:
// - 6 Degrees of Freedom flight
// - Boost system with recharge
// - Analog stick dead zones and response curves
// - Speed limiting
// - Event system for UI/VFX hookup
// ============================================================================

using UnityEngine;
using UnityEngine.InputSystem;
using System;

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

    // Boost events for UI/VFX hookup
    public event Action<float> OnBoostChanged; // Reports 0-1 fill percentage
    public event Action OnBoostDepleted;
    public event Action OnBoostRecharged;

    private bool wasBoostEmpty = false;

    [Header("Rotation")]
    [SerializeField] private float pitchSpeed = 100f;
    [SerializeField] private float yawSpeed = 100f;

    [Header("Input Settings")]
    [SerializeField] private float stickDeadZone = 0.15f;
    [SerializeField] private AnimationCurve movementResponseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve lookResponseCurve = AnimationCurve.Linear(0, 0, 1, 1);

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

    /// <summary>
    /// Applies movement forces based on player input.
    /// Called every physics frame (FixedUpdate).
    /// </summary>
    private void HandleMovement()
    {
        // Process input through curves and deadzone
        Vector2 processedMove = ProcessInput(moveInput, stickDeadZone, movementResponseCurve);

        // Forward/backward thrust
        if (processedMove.y != 0)
        {
            rb.AddRelativeForce(Vector3.forward * processedMove.y * thrustForce);
        }

        // Strafe left/right
        if (processedMove.x != 0)
        {
            rb.AddRelativeForce(Vector3.right * processedMove.x * strafeForce);
        }

        // Vertical movement (keep raw for buttons)
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
        // Process look input through curve
        Vector2 processedLook = ProcessInput(lookInput, stickDeadZone, lookResponseCurve);

        // Pitch (look up/down)
        if (processedLook.y != 0)
        {
            float pitch = -processedLook.y * pitchSpeed * Time.fixedDeltaTime;
            rb.transform.Rotate(Vector3.right, pitch, Space.Self);
        }

        // Yaw (look left/right)
        if (processedLook.x != 0)
        {
            float yaw = processedLook.x * yawSpeed * Time.fixedDeltaTime;
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
        float previousBoost = currentBoost;

        if (!isPrimaryBoosting && currentBoost < maxBoost)
        {
            currentBoost += boostRechargeRate * Time.deltaTime;
            currentBoost = Mathf.Min(currentBoost, maxBoost);

            // Trigger recharged event when full
            if (previousBoost < maxBoost && currentBoost >= maxBoost)
            {
                OnBoostRecharged?.Invoke();
            }
        }

        // Check for depletion
        if (currentBoost <= 0 && !wasBoostEmpty)
        {
            wasBoostEmpty = true;
            OnBoostDepleted?.Invoke();
        }
        else if (currentBoost > 0 && wasBoostEmpty)
        {
            wasBoostEmpty = false;
        }

        // Always notify of boost changes (for UI gauge)
        OnBoostChanged?.Invoke(currentBoost / maxBoost);
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

    /// <summary>
    /// Processes raw analog input through dead zone and response curve.
    /// Returns smoothed, refined input value.
    /// </summary>
    private Vector2 ProcessInput(Vector2 rawInput, float deadZone, AnimationCurve curve)
    {
        // Apply dead zone
        if (rawInput.magnitude < deadZone)
            return Vector2.zero;

        // Normalize direction and get magnitude
        Vector2 direction = rawInput.normalized;
        float magnitude = rawInput.magnitude;

        // Clamp magnitude to 1.0 max
        magnitude = Mathf.Clamp01(magnitude);

        // Apply response curve
        magnitude = curve.Evaluate(magnitude);

        return direction * magnitude;
    }

    // Public properties for external access
    public float CurrentBoost => currentBoost;
    public float MaxBoost => maxBoost;
    public bool IsBoosting => isPrimaryBoosting;
}