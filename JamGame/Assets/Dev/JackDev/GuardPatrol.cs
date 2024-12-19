using UnityEngine;
using System.Collections;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for patrol path
    public float speed = 2f; // Movement speed
    public float waitTime = 2f; // Default wait time at waypoints
    public Transform fieldOfView; // Reference to the field of view object
    private int currentWaypointIndex = 0;
    private bool isReversing = false;
    private bool isWaiting = false;
    private SpriteRenderer spriteRenderer; // Reference to the guard's SpriteRenderer
    private Vector3 lastPosition; // To track movement direction

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Handle sprite flipping
        FlipSprite();

        // Rotate the field of view to face the next waypoint
        RotateFieldOfView(targetPosition);

        // If close to the waypoint, start wait behavior
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Check if the waypoint has a "LookAround" tag
            if (targetWaypoint.CompareTag("LookAround"))
            {
                StartCoroutine(LookAround());
            }
            else
            {
                StartCoroutine(WaitAtWaypoint(0.25f)); // Default short wait
            }
        }
    }

    private void FlipSprite()
    {
        Vector3 direction = transform.position - lastPosition; // Calculate movement direction
        lastPosition = transform.position;

        // Flip sprite based on horizontal movement
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Moving left
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Moving right
        }
    }

    private void RotateFieldOfView(Vector3 targetPosition)
    {
        if (fieldOfView != null)
        {
            Vector3 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set rotation of FOV object
            fieldOfView.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private IEnumerator LookAround()
    {
        isWaiting = true;

        // Gradually rotate the FOV 360°
        float totalRotation = 0f;
        float rotationSpeed = 180f; // Degrees per second

        while (totalRotation < 360f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            fieldOfView.Rotate(0, 0, rotationStep);
            totalRotation += rotationStep;
            yield return null; // Wait for the next frame
        }

        // Ensure the FOV ends back at its original rotation
        fieldOfView.localRotation = Quaternion.identity;

        // Wait for a moment after the 360° rotation
        yield return new WaitForSeconds(waitTime);

        // Resume patrol
        isWaiting = false;

        // Go to the next waypoint or reverse direction
        if (!isReversing)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex -= 2; // Step back to reverse
                isReversing = true;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Step forward to reverse again
                isReversing = false;
            }
        }
    }

    private IEnumerator WaitAtWaypoint(float duration)
    {
        isWaiting = true;
        yield return new WaitForSeconds(duration);
        isWaiting = false;

        // Go to the next waypoint or reverse direction
        if (!isReversing)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex -= 2; // Step back to reverse
                isReversing = true;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1; // Step forward to reverse again
                isReversing = false;
            }
        }
    }
}
