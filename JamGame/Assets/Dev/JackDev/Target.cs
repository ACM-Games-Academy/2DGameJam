using System.Collections;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelData
{
    public int levelIndex;
    public bool isCompleted;
}

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
    public int currentLevel;               // Current level index
    public SaveData saveData;

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
            StartCoroutine(FinishLevel());
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

    public IEnumerator FinishLevel()
    {
        // Delay to allow the animation to play
        yield return new WaitForSeconds(1f);









        MarkAsComplete();
    }

    public void MarkAsComplete()
    {
        if (currentLevel == 1) { saveData.level1Complete = true; }
        if (currentLevel == 2) { saveData.level2Complete = true; }
        if (currentLevel == 3) { saveData.level3Complete = true; }
        if (currentLevel == 4) { saveData.level4Complete = true; }
        if (currentLevel == 5) { saveData.level5Complete = true; }
        if (currentLevel == 6) { saveData.level6Complete = true; }
        if (currentLevel == 7) { saveData.level7Complete = true; }
        if (currentLevel == 8) { saveData.level8Complete = true; }
        if (currentLevel == 9) { saveData.level9Complete = true; }
        if (currentLevel == 10) { saveData.level10Complete = true; }
        if (currentLevel == 11) { saveData.level11Complete = true; }
        if (currentLevel == 12) { saveData.level12Complete = true; }
        saveData.SaveProgress();

    }
}