using UnityEngine;

public class Combat : MonoBehaviour
{
    private Detection[] detections;

    public bool isDetected = false;

    void Start()
    {

        detections = FindObjectsOfType<Detection>();
    }

    void Update()
    {

        isDetected = false;


        foreach (Detection detection in detections)
        {
            if (detection.IsPlayerDetected)
            {
                isDetected = true;
                break;
            }
        }


        if (isDetected)
        {
            foreach (Detection detection in detections)
            {
                detection.SetMeshColor(Color.red);
            }
        }
    }
}
