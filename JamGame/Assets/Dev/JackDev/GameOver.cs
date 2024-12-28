using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI components

public class GameOver : MonoBehaviour
{
    private Combat combat;
    private Timer timer;

    public Image fadeOutImage; // Publicly assignable in the Inspector
    private GameObject player;


    // End UI
    public GameObject endScreen;



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
        if (combat.isDetected)
        {
            StartCoroutine(FadeOutImage());

        }
    }

    private IEnumerator FadeOutImage()
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

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
       
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
