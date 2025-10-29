using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Smoothly move to target position
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            followSpeed * Time.deltaTime
        );

        // Smoothly rotate to match target orientation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            rotationSpeed * Time.deltaTime
        );
    }
}