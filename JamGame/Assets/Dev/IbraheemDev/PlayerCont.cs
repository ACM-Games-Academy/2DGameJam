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
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition; 

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position; 
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
        // Move the player
        walkVect = new Vector2(horizontalInput, verticalInput);
        walkVect.Normalize();
        myRb.velocity = walkVect * walkSpeed;


        Vector3 currentPosition = transform.position;
        Vector3 direction = currentPosition - lastPosition;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Moving left
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Moving right
        }
        lastPosition = currentPosition;
    }
}
