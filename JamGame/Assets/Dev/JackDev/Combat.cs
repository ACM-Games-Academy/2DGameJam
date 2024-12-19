using UnityEngine;

public class Combat : MonoBehaviour
{
    private Detection[] detections;

    public bool isDetected = false;

    void Start()
    {
        // Find all Detection components in the scene
        detections = FindObjectsOfType<Detection>();
    }

    void Update()
    {
        // Reset isDetected to false at the start of each frame
        isDetected = false;

        // Check each Detection script to see if the player is detected
        foreach (Detection detection in detections)
        {
            if (detection.IsPlayerDetected)
            {
                isDetected = true;
                break; // Exit the loop early if any detection is true
            }
        }

        // If the player is detected by any camera, notify all Detection scripts
        if (isDetected)
        {
            foreach (Detection detection in detections)
            {
                detection.SetMeshColor(Color.red);
            }
        }
    }
}
