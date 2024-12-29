using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Combat combat;
    private Timer timer;

    public Image fadeOutImage; // Publicly assignable in the Inspector
    private GameObject player;
    [HideInInspector] public bool hasWon;
    public Audio Audio;

    // Flags to ensure sounds are played only once
    private bool hasPlayedLoseSound = false;
    private bool hasPlayedWinSound = false;

    // End UI
    public GameObject endScreen;
    public GameObject winScreen;
    [SerializeField] private string nextSceneName; // Scene name

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = GetComponent<Timer>();
        combat = GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Play lose sound and start fade-out if detected
        if (combat.isDetected && !hasPlayedLoseSound)
        {
            hasPlayedLoseSound = true; // Mark as played
            Audio.PlayLoseSound();
            StartCoroutine(FadeOutImageLose());
        }

        // Play win sound and start fade-out if the player has won
        if (hasWon && !hasPlayedWinSound)
        {
            hasPlayedWinSound = true; // Mark as played
            Audio.PlayWinSound();
            StartCoroutine(FadeOutImageWin());
        }
    }

    private IEnumerator FadeOutImageLose()
    {
        if (fadeOutImage != null)
        {
            float duration = 1f; // Duration of the fade-out in seconds
            float elapsedTime = 0f;

            Color originalColor = fadeOutImage.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Linearly interpolate the alpha value
                float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);
                fadeOutImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                yield return null; // Wait for the next frame
            }

            // Ensure the alpha is set to 0 at the end
            fadeOutImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            EndScreen();
        }
    }

    public void EndScreen()
    {
        player.SetActive(false);
        endScreen.SetActive(true);
    }

    private IEnumerator FadeOutImageWin()
    {
        if (fadeOutImage != null)
        {
            float duration = 1f; // Duration of the fade-out in seconds
            float elapsedTime = 0f;

            Color originalColor = fadeOutImage.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Linearly interpolate the alpha value
                float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / duration);
                fadeOutImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                yield return null; // Wait for the next frame
            }

            // Ensure the alpha is set to 0 at the end
            fadeOutImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            WinScreen();
        }
    }

    public void WinScreen()
    {
        player.SetActive(false);
        winScreen.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel()
    {
       SceneManager.LoadScene(nextSceneName);
    }

}
