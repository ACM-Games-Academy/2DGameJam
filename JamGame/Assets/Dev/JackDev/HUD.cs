using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    private Combat combat;
    private Timer timer;

    public TextMeshProUGUI detected;
    public TextMeshProUGUI timerText;


    private void Start()
    {
        combat = FindObjectOfType<Combat>();
        timer = FindObjectOfType<Timer>();
        detected.text = (" ");

       
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimerHUD();
        UpdateDetectionHUD();

    }
    private void UpdateDetectionHUD()
    {
        if (combat.isDetected)
        {
            detected.text = ("DETECTED!");
            detected.color = Color.red;

        }
        else { detected.text = ("HIDDEN"); }
    }

    private void UpdateTimerHUD()
    {
        // Round the current time to an integer and display it
        int seconds = Mathf.CeilToInt(timer.currentTime); // Rounds up to ensure it doesn't display 0 prematurely
        timerText.text = $"{seconds}";

        if (seconds < 10) {timerText.color = Color.red;}

    }
}
