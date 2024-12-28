using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private Detection[] detections;

    public float maxTime = 60f; // Maximum time for the timer
    [HideInInspector] public float currentTime;
    public float fovIncreaseRate = 10f; // Maximum range to increase viewRange by

    // Start is called before the first frame update
    void Start()
    {
        // Find all Detection components in the scene
        detections = FindObjectsOfType<Detection>();
        currentTime = maxTime; // Initialize the timer to the maximum time
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateViewRange();
        }

        // Check if the player is detected by any Detection component
        foreach (Detection detection in detections)
        {
            if (detection.IsPlayerDetected)
            {
                EndGame();
                break;
            }
        }
    }

    private void UpdateViewRange()
    {
        // Calculate the proportion of time left
        float timeFraction = currentTime / maxTime;

        // Calculate the new viewRange for detections
        float currentSightIncrease = fovIncreaseRate * (1 - timeFraction);

        // Update the viewRange for all Detection components
        foreach (Detection detection in detections)
        {
            detection.viewRange += currentSightIncrease * Time.deltaTime;
        }
    }

    private void EndGame()
    {
       
    }
}
