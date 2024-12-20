using System.Collections;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject cameraPositionOne;
    public GameObject cameraPositionTwo;
    public GameObject cameraPositionThree;
    public Transform cameraTransform; // Reference to the camera's transform

    public float lerpDuration = 1.0f; // Duration of the lerp in seconds

    void Start()
    {
        // Start the camera at position one
        cameraTransform.position = cameraPositionOne.transform.position;
    }

    public void MoveToLevels()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionTwo.transform.position));
    }

    public void MoveToMenu()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionOne.transform.position));
    }

    public void MoveToCredits()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionThree.transform.position));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LerpCameraPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = cameraTransform.position;
        float elapsedTime = 0;

        while (elapsedTime < lerpDuration)
        {
            // Interpolate between the start and target positions
            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / lerpDuration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the camera ends exactly at the target position
        cameraTransform.position = targetPosition;
    }
}
