using System.Collections;
using UnityEngine;

public class OscillatingMover : MonoBehaviour
{
    [SerializeField] private float activationTime; // Time to start moving
    [SerializeField] private int level; // Level at which the object moves
    [SerializeField] private Vector2 moveDirection; // Direction of movement
    [SerializeField] private float moveSpeed; // Speed of movement
    [SerializeField] private float moveDistance; // Distance to move in one direction
    [SerializeField] private float pauseDuration; // Pause duration at each end

    private bool hasActivated = false; // Tracks if the object has already activated for the current condition
    private Coroutine moveCoroutine; // Tracks the currently running movement coroutine
    private Vector3 startPosition; // Stores the original position of the object

    void Start()
    {
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        // Check if the object should start moving
        if (GameInfo.GameMode == level && Timer.GetTime() >= activationTime && !hasActivated)
        {
            hasActivated = true; // Prevent multiple activations for the same condition
            moveCoroutine = StartCoroutine(Oscillate());
        }

        // Stop moving and reset the activation flag if the GameMode changes
        if (GameInfo.GameMode != level)
        {
            hasActivated = false;

            // Stop the currently running coroutine if it exists
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null; // Clear the reference
            }

            // Reset the object's position to the starting position
            transform.position = startPosition;
        }
    }

    private IEnumerator Oscillate()
    {
        while (true)
        {
            // Move in the specified direction
            yield return MoveInDirection(moveDirection);

            // Pause at the end
            yield return new WaitForSeconds(pauseDuration);

            // Move back to the starting position
            yield return MoveInDirection(-moveDirection);

            // Pause at the starting position
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private IEnumerator MoveInDirection(Vector2 direction)
    {
        Vector3 targetPosition = transform.position + (Vector3)(direction.normalized * moveDistance);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}