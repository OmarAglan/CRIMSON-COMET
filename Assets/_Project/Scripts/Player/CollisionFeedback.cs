using UnityEngine;

public class CollisionFeedback : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minImpactVelocity = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get impact velocity
        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed > minImpactVelocity)
        {
            // Print to console (we'll replace with sound later)
            Debug.Log($"IMPACT! Speed: {impactSpeed:F1}");
        }
    }
}