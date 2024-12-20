using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : MonoBehaviour
{
    private Rigidbody2D myRb;
    private float horizontalInput;
    private float verticalInput;
    public float walkSpeed;
    private Vector2 walkVect;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        walkVect = new Vector2 (horizontalInput, verticalInput);
        walkVect.Normalize();
        myRb.velocity = walkVect * walkSpeed;
    }
}
