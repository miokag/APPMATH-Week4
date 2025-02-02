using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private Transform targetTransform;
    private float killDistance; // This will be set from the turret's upgrade

    public void Initialize(Vector2 direction, float speed, Transform target, float distance)
    {
        moveDirection = direction;
        moveSpeed = speed;
        targetTransform = target;
        killDistance = distance; // Set the kill distance from turret
    }

    void Update()
    {
        // Move the bullet in the direction of the target
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the bullet hits the target
        if (targetTransform != null && IsTargetHit())
        {
            Debug.Log("Enemy hit!");
            Destroy(gameObject); // Destroy the bullet
            Destroy(targetTransform.gameObject); // Destroy the enemy
            if (GameManager.Instance != null)  // Check if GameManager is properly initialized
            {
                GameManager.Instance.AddGold(5);  // Award gold
            }
        }

        // Destroy bullet if it goes out of range (optional, adjust based on game design)
        if (transform.position.magnitude > 7f)
        {
            Destroy(gameObject);
        }
    }

    private bool IsTargetHit()
    {
        if (targetTransform == null) return false;

        // Calculate the distance between the bullet and the target
        float distance = Vector2.Distance(transform.position, targetTransform.position);

        // Check if the bullet has reached its kill distance
        return distance <= killDistance;
    }
}