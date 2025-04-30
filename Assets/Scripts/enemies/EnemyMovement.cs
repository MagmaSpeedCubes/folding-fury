using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 direction;
    private float speed;

    public void SetMovement(Vector2 moveDirection, float moveSpeed)
    {
        direction = moveDirection.normalized; // Normalize the direction
        speed = moveSpeed;
    }

    void Update()
    {
        // Move the enemy in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }
}