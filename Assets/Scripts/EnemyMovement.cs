using UnityEngine;

public enum MovementType { Quadratic, Cubic }

public class EnemyMovement : MonoBehaviour
{
    private Transform targetLocation;
    private MovementType movementType;
    private float moveSpeed;
    private float targetThreshold = 0.1f;  // Threshold for "reaching" the target

    // Initialize the movement with target, movement type, and speed
    public void Initialize(Transform target, MovementType type, float speed)
    {
        targetLocation = target;
        movementType = type;
        moveSpeed = speed;
    }

    void Update()
    {
        if (targetLocation == null) return;

        Vector3 direction = targetLocation.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= targetThreshold)
        {
            Destroy(gameObject);  // Destroy the enemy when it reaches the target
            GameManager.Instance.TakeDamage();
            return;
        }

        direction = direction.normalized * moveSpeed * Time.deltaTime; // Speed-based movement
        transform.position += direction;
    }

}