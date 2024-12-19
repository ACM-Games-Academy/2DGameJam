using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class Detection : MonoBehaviour
{
    [SerializeField] private float viewRange = 5f;      // Distance of the cone
    [SerializeField] private float fovAngle = 90f;      // Field of view
    [SerializeField] private int resolution = 50;       // Number of rays

    [SerializeField] private LayerMask obstacleMask;    // Layers to check for blocking
    private GameObject player;
    [SerializeField] private float detectionTime = 3f; // Time required to detect player
    [SerializeField] private int raySamples = 5;            
    [SerializeField] private float detectionThreshold = 0.5f; 

    private Mesh visionMesh;
    public float detectionProgress = 0f;
    private Coroutine detectionCoroutine = null;
    private int detectionCount = 0;
    private Combat combat;

    public bool IsPlayerDetected = false; // Public flag for detection status

    private void Start()
    {
        visionMesh = new Mesh();

        player = GameObject.FindWithTag("Player");
        combat = FindObjectOfType<Combat>();
        GetComponent<MeshFilter>().mesh = visionMesh;
        UpdateMeshColor(Color.white); 
    }

    private void Update()
    {

        UpdateVisionMesh();
        if (detectionCount <= 1)
        {
            
            CheckPlayerInVision();
        }
    }

    private void UpdateVisionMesh()
    {
        Vector3[] vertices = new Vector3[resolution + 2];
        int[] triangles = new int[resolution * 3];

        vertices[0] = Vector3.zero; 

        float angleStep = fovAngle / resolution;
        float startAngle = -fovAngle / 2;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 dir = DirFromAngle(angle).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRange, obstacleMask);
            Vector3 hitPoint = hit.collider ? (Vector3)hit.point : transform.position + dir * viewRange;

            Debug.DrawRay(transform.position, dir * viewRange, hit.collider ? Color.green : Color.yellow); // Visualize rays

            vertices[i + 1] = transform.InverseTransformPoint(hitPoint);

            if (i < resolution)
            {
                int triangleIndex = i * 3;
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = i + 1;
                triangles[triangleIndex + 2] = i + 2;
            }
        }

        visionMesh.Clear();
        visionMesh.vertices = vertices;
        visionMesh.triangles = triangles;
        visionMesh.RecalculateNormals();
    }

    private Vector3 DirFromAngle(float angle)
    {
        float rad = Mathf.Deg2Rad * (angle + transform.eulerAngles.z);
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    }

    private void CheckPlayerInVision()
    {
        if (player == null) return;

        Collider2D playerCollider = player.GetComponent<Collider2D>();

        Bounds playerBounds = playerCollider.bounds;
        int visiblePoints = 0;
        int totalPoints = raySamples * raySamples;

        // Cast rays toward points in the player's bounds
        for (int x = 0; x < raySamples; x++)
        {
            for (int y = 0; y < raySamples; y++)
            {
                Vector3 point = new Vector3(
                    Mathf.Lerp(playerBounds.min.x, playerBounds.max.x, x / (float)(raySamples - 1)),
                    Mathf.Lerp(playerBounds.min.y, playerBounds.max.y, y / (float)(raySamples - 1)),
                    playerBounds.center.z
                );

                Vector3 directionToPoint = (point - transform.position).normalized;
                float angleToPoint = Vector3.Angle(transform.right, directionToPoint);

                if (angleToPoint <= fovAngle / 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPoint, viewRange, obstacleMask);
                    Debug.DrawRay(transform.position, directionToPoint * viewRange, hit.collider ? Color.red : Color.blue);

                    if (hit.collider != null && hit.collider.gameObject == player)
                    {
                        visiblePoints++;
                    }
                }
            }
        }

        float visibility = (float)visiblePoints / totalPoints;

        if (visibility >= detectionThreshold)
        {
            // Start or continue detection if not already happening
            if (detectionCoroutine == null)
            {
                detectionCoroutine = StartCoroutine(DetectPlayer());
            }
        }
        else
        {
            // Reset detection progress if player is no longer visible
            if (detectionCoroutine != null)
            {
                StopCoroutine(detectionCoroutine);
                detectionCoroutine = null;
                detectionProgress = 0f;
                IsPlayerDetected = false; 
                UpdateMeshColor(Color.white); 
                Debug.Log("Player left vision. Detection progress reset.");
            }
        }
    }

    private IEnumerator DetectPlayer()
    {
        Debug.Log("Starting detection countdown...");

        // Gradually increase detection progress
        while (detectionProgress < detectionTime)
        {
            // Check if the player is already detected to avoid further updates
            if (IsPlayerDetected)
            {
                UpdateMeshColor(Color.red);
                yield break; 
            }

            detectionProgress += Time.deltaTime;
            float t = detectionProgress / detectionTime;
            Color meshColor = Color.Lerp(Color.yellow, Color.red, t);
            UpdateMeshColor(meshColor);

            yield return null;
        }

        // Detection completed
        IsPlayerDetected = true;
        Debug.Log("Player detected!");
        UpdateMeshColor(Color.red); 
        detectionCoroutine = null;
    }


    private void UpdateMeshColor(Color color)
    {
        if (GetComponent<MeshRenderer>() != null)
        {
            // Set a constant alpha value for transparency
            Color transparentColor = color;
            transparentColor.a = 0.5f; 
            GetComponent<MeshRenderer>().material.color = transparentColor;
        }
    }

    public void SetMeshColor(Color color)
    {
        UpdateMeshColor(color);
    }

}
