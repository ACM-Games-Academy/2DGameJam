using System.Collections;
using UnityEngine;
using static GuardPatrol;

public class CameraEnemy : MonoBehaviour
{
    public float speed = 1;     // Speed
    public float minAngle = 0;  // Minimum angle
    public float maxAngle = 90; // Maximum angle#

    public float alertAppearTime = 1f; // Time the alert appears
    public GameObject alert;
    public Audio Audio;

    private Combat combat;
    private Detection detection;
    private Coroutine alertCoroutine; 
    private int timesAlerted;

    public Quaternion alertFixedRotation; // Fixed rotation for the alert

    public void Awake()
    {
        alert.SetActive(false);
        alertFixedRotation = alert.transform.rotation; 
        combat = FindObjectOfType<Combat>();
        detection = GetComponentInChildren<Detection>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera
        float t = Mathf.PingPong(Time.time * speed, 1);
        float rY = Mathf.Lerp(minAngle, maxAngle, t);
        transform.rotation = Quaternion.Euler(0, 0, rY);

        // Lock the alert's rotation
        alert.transform.rotation = alertFixedRotation;

        // Check for detection
        if (combat.isDetected && timesAlerted < 1)
        {
            if (alertCoroutine == null)
            {
                alertCoroutine = StartCoroutine(Alert());
            }
        }

        if (detection != null && detection.IsBeingDetected)
        {
            Audio.PlayCameraSearchingSound();
        }
    }

    private IEnumerator Alert()
    {
        timesAlerted++;
        alert.SetActive(true); // Show the alert
        yield return new WaitForSeconds(alertAppearTime); // Wait for the defined time
        alert.SetActive(false); // Hide the alert
        alertCoroutine = null; // Reset the coroutine reference
    }
}
