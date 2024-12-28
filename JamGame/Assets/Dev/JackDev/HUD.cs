using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class HUD : MonoBehaviour
{
    private Combat combat;
    private Timer timer;

    public Slider timerSlider; 

    private void Start()
    {
        combat = FindObjectOfType<Combat>();
        timer = FindObjectOfType<Timer>();

        timerSlider.maxValue = timer.maxTime; 
        timerSlider.value = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerHUD();
    }

    private void UpdateTimerHUD()
    {
        if (timerSlider != null)
        {
            timerSlider.value = timer.maxTime - timer.currentTime;
            int seconds = Mathf.CeilToInt(timer.currentTime); 
        }
    }
}
