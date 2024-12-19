using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    private Combat combat;

    public TextMeshProUGUI detected;


    private void Start()
    {
        combat = FindObjectOfType<Combat>(); 
        detected.text = (" ");
    }


    // Update is called once per frame
    void Update()
    {
        if (combat.isDetected)
        {
            detected.text = ("DETECTED");
            detected.color = Color.red;

        }
        else { detected.text = (" "); }
    }
}
