using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // This will store our physics component
    private Rigidbody rb;

    // This runs once when the game starts
    void Start()
    {
        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();
        
        // Test message to confirm the script is working
        Debug.Log("Player Controller Started!");
    }

    // This runs every physics frame (50 times per second by default)
    void FixedUpdate()
    {
        // We'll add movement code here soon
    }
}