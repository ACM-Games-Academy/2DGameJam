using UnityEngine;
using System.Collections;

public class GuardPatrol : MonoBehaviour
{
    public enum GuardState { Patrolling, Alert, Searching }
    public GuardState currentState = GuardState.Patrolling;

    private Detection detection;
    public Transform[] waypoints;       // Array of waypoints for patrol path
    public float speed = 2f;            // Movement speed
    public float searchSpeed = 0f;
    public Transform fieldOfView;       // Reference to the field of view object
    public Sprite sprite1;              // First animation sprite
    public Sprite sprite2;
    public Sprite searching;
    public Sprite alert;
    public Audio Audio;

    private int currentWaypointIndex = 0;
    private bool isReversing = false;
    private bool isWaiting = false;
    private SpriteRenderer spriteRenderer; // Reference to the guard's SpriteRenderer
    private Vector3 lastPosition;          // To track movement direction
    private Combat combat;
    private bool alertTriggered = false;

    private GuardState lastState = GuardState.Patrolling; // To track state changes
    private bool searchingSoundPlayed = false; // Prevents re-triggering the sound while it's already playing

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        combat = FindObjectOfType<Combat>();

        // Get the Detection component from the child object
        detection = GetComponentInChildren<Detection>();

        lastPosition = transform.position;

        // Start sprite animation for patrolling
        StartCoroutine(SwitchPatrolSprites());
    }

    private void Update()
    {
        // Check for detection and set state accordingly
        if (detection != null && detection.IsBeingDetected)
        {
            currentState = GuardState.Searching; // Set to Searching while player is detected
        }
        else if (combat.isDetected && !alertTriggered)
        {
            StartCoroutine(TriggerAlert()); // Trigger Alert if detection occurred
        }
        else
        {
            currentState = GuardState.Patrolling; // Default state
        }

        // Perform patrol actions
        Patrol();

        // Update the sprite and handle audio based on the current state
        UpdateState();
    }

    private void UpdateState()
    {
        // Check if the state has changed
        if (currentState != lastState)
        {
            lastState = currentState;

            switch (currentState)
            {
                case GuardState.Patrolling:
                    
                    searchingSoundPlayed = false; 
                    break;

                case GuardState.Alert:
                    
                    searchingSoundPlayed = false; 
                    break;

                case GuardState.Searching:
                    if (!searchingSoundPlayed) 
                    {
                        Audio.PlaySearchingSound();
                        searchingSoundPlayed = true;
                    }
                    break;
            }

            UpdateSprite();
        }
    }

    private void Patrol()
    {
        if (isWaiting) return;

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
        Vector3 direction = transform.position - lastPosition;
        lastPosition = transform.position;

        if (direction.x < 0)
            spriteRenderer.flipX = true; // Moving left
        else if (direction.x > 0)
            spriteRenderer.flipX = false; // Moving right
    }

    private void RotateFieldOfView(Vector3 targetPosition)
    {
        if (fieldOfView == null) return;

        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fieldOfView.rotation = Quaternion.Euler(0, 0, angle);
    }

    private IEnumerator WaitAtWaypoint(float duration)
    {
        isWaiting = true;
        yield return new WaitForSeconds(duration);
        isWaiting = false;

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

    private IEnumerator LookAround()
    {
        isWaiting = true;

        float totalRotation = 0f;
        float rotationSpeed = searchSpeed;

        while (totalRotation < 360f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            fieldOfView.Rotate(0, 0, rotationStep);
            totalRotation += rotationStep;
            yield return null;
        }

        fieldOfView.localRotation = Quaternion.identity;
        isWaiting = false;

        if (!isReversing)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex -= 2;
                isReversing = true;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1;
                isReversing = false;
            }
        }
    }

    private IEnumerator SwitchPatrolSprites()
    {
        while (true)
        {
            // Only animate patrol sprites when in the Patrolling state
            if (currentState == GuardState.Patrolling)
            {
                spriteRenderer.sprite = sprite1;
                yield return new WaitForSeconds(0.5f);
                spriteRenderer.sprite = sprite2;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator TriggerAlert()
    {
        currentState = GuardState.Alert;
        alertTriggered = true;

        // Temporarily switch to alert sprite
        spriteRenderer.sprite = alert;
        yield return new WaitForSeconds(1f);

        // Reset to patrolling state
        currentState = GuardState.Patrolling;
        alertTriggered = false;
    }

    private void UpdateSprite()
    {
        switch (currentState)
        {
            case GuardState.Patrolling:
                
                break;

            case GuardState.Alert:
                spriteRenderer.sprite = alert;
                break;

            case GuardState.Searching:
                spriteRenderer.sprite = searching;
                break;
        }
    }
}
