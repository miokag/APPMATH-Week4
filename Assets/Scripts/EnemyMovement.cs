using UnityEngine;

public enum MovementType { Quadratic, Cubic }

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private MovementType movementType;
    private float startTime;
    private Vector3 startPoint;
    private Vector3 control1, control2; // Control points for bezier curves

    // Adjustable movement properties
    [SerializeField] private float quadraticSpeed = 4f;  
    [SerializeField] private float cubicSpeed = 4f;      
    [SerializeField] private float quadraticJourneyTime = 4.5f; 
    [SerializeField] private float cubicJourneyTime = 5f;   

    // Randomness range for control points
    [SerializeField] private float controlPointRange = 3f; // Range within which control points will be random

    public void Initialize(Transform targetLocation, MovementType moveType)
    {
        target = targetLocation;
        movementType = moveType;
        startPoint = transform.position;
        startTime = Time.time;

        // Generate random control points within the defined range
        control1 = startPoint + new Vector3(Random.Range(-controlPointRange, controlPointRange), Random.Range(-controlPointRange, controlPointRange), Random.Range(-controlPointRange, controlPointRange));
        control2 = target.position + new Vector3(Random.Range(-controlPointRange, controlPointRange), Random.Range(-controlPointRange, controlPointRange), Random.Range(-controlPointRange, controlPointRange));
    }

    void Update()
    {
        float journeyTime = (movementType == MovementType.Quadratic) ? quadraticJourneyTime : cubicJourneyTime;
        float speed = (movementType == MovementType.Quadratic) ? quadraticSpeed : cubicSpeed;
        
        float t = (Time.time - startTime) / journeyTime;
        t = Mathf.Clamp01(t); // Ensure t stays in range [0,1]

        if (t >= 1f)
        {
            ReachTarget();
            return;
        }

        switch (movementType)
        {
            case MovementType.Quadratic:
                transform.position = QuadraticBezier(startPoint, control1, target.position, t);
                break;
            case MovementType.Cubic:
                transform.position = CubicBezier(startPoint, control1, control2, target.position, t);
                break;
        }
    }

    private Vector3 QuadraticBezier(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 2) * start +
               2 * (1 - t) * t * control +
               t * t * end;
    }

    private Vector3 CubicBezier(Vector3 start, Vector3 control1, Vector3 control2, Vector3 end, float t)
    {
        return Mathf.Pow(1 - t, 3) * start +
               3 * Mathf.Pow(1 - t, 2) * t * control1 +
               3 * (1 - t) * t * t * control2 +
               t * t * t * end;
    }

    private void ReachTarget()
    {
        GameManager.Instance.TakeDamage();
        Destroy(gameObject);
    }
}
