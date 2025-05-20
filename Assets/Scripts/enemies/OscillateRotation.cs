using UnityEngine;

public class OscillateRotation : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // Speed of the oscillation
    [SerializeField] private float angle = 30f; // Maximum angle of oscillation

    private Quaternion startRotation; // The initial rotation of the object

    void Start()
    {
        // Store the initial rotation
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Calculate the oscillation angle using a sine wave
        float oscillation = Mathf.Sin(Time.time * speed) * angle;

        // Apply the oscillation to the rotation
        transform.rotation = startRotation * Quaternion.Euler(0, 0, oscillation);
    }
}