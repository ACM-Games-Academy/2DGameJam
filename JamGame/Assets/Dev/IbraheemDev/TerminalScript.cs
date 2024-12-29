using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    public GameObject[] Door;
    public GameObject[] Bodyguards;
    public Audio Audio;
    // Start is called before the first frame update
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
    if (other.gameObject.CompareTag("Player")){
        if(Door.Length > 0){
            foreach (GameObject DOOR in Door){

                    Audio.PlayButtonPressSound();
                Destroy(DOOR);
            }
        }
        foreach (GameObject GUY in Bodyguards){
            GUY.GetComponent<GuardPatrol>().enabled = true;
        }
    }    

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
