using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] deathSprites;        // Array of sprites for death animation
    public Sprite[] idleSprites;         // Array of sprites for idle animation
    private GameObject player;           // Reference to the player object
    public float playerDistanceToKill = 0;

    private int currentSpriteIndex = 0;  // Tracks the current sprite index
    private Coroutine currentCoroutine;  // Tracks the currently running coroutine
    private bool isPlayerInRange = false; // Tracks if player is in range

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        SwitchToAnimation(idleSprites);
    }

    void Update()
    {

        float playerDistance = Vector2.Distance(player.transform.position, gameObject.transform.position);

        if (playerDistance < playerDistanceToKill && !isPlayerInRange)
        {
            isPlayerInRange = true; 
            SwitchToAnimation(deathSprites); 
        }
        else if (playerDistance >= playerDistanceToKill && isPlayerInRange)
        {
            isPlayerInRange = false; 
            SwitchToAnimation(idleSprites); 
        }
    }

    private void SwitchToAnimation(Sprite[] sprites)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); 
        }
        currentCoroutine = StartCoroutine(PlayAnimation(sprites)); 
    }

    private IEnumerator PlayAnimation(Sprite[] sprites)
    {
        while (true)
        {
            if (sprites.Length > 0)
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length; 
                spriteRenderer.sprite = sprites[currentSpriteIndex];
            }
            yield return new WaitForSeconds(0.5f); 
        }
    }
}
