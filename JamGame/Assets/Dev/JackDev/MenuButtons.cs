using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject cameraPositionOne;
    public GameObject cameraPositionTwo;
    public GameObject cameraPositionThree;
    public Transform cameraTransform; // Reference to the camera's transform

    public float lerpDuration = 1.0f; // Duration of the lerp in seconds

    [Header("Locked levels")]

    public SaveData saveData;

    public GameObject levelTwoLocked;
    public GameObject levelThreeLocked;
    public GameObject levelFourLocked;
    public GameObject levelFiveLocked;
    public GameObject levelSixLocked;
    public GameObject levelSevenLocked;
    public GameObject levelEightLocked;
    public GameObject levelNineLocked;
    public GameObject levelTenLocked;
    public GameObject levelElevenLocked;
    public GameObject levelTwelveLocked;



    void Start()
    {
        // Start the camera at position one
        cameraTransform.position = cameraPositionOne.transform.position;
        saveData.LoadProgress();
        UpdateLockedLevels();
        
    }

    public void MoveToLevels()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionTwo.transform.position));
    }

    public void MoveToMenu()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionOne.transform.position));
    }

    public void MoveToCredits()
    {
        StartCoroutine(LerpCameraPosition(cameraPositionThree.transform.position));
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator LerpCameraPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = cameraTransform.position;
        float elapsedTime = 0;

        while (elapsedTime < lerpDuration)
        {
            cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / lerpDuration);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        cameraTransform.position = targetPosition; 
    }

    // ===== Load levels =====

    void UpdateLockedLevels()
    {
        if (saveData.level1Complete) { levelTwoLocked.SetActive(false); }
        if (saveData.level2Complete) { levelThreeLocked.SetActive(false); }
        if (saveData.level3Complete) { levelFourLocked.SetActive(false); }
        if (saveData.level4Complete) { levelFiveLocked.SetActive(false); }
        if (saveData.level5Complete) { levelSixLocked.SetActive(false); }
        if (saveData.level6Complete) { levelSevenLocked.SetActive(false); }
        if (saveData.level7Complete) { levelEightLocked.SetActive(false); }
        if (saveData.level8Complete) { levelNineLocked.SetActive(false); }
        if (saveData.level9Complete) { levelTenLocked.SetActive(false); }
        if (saveData.level10Complete) { levelElevenLocked.SetActive(false); }
        if (saveData.level11Complete) { levelTwelveLocked.SetActive(false); }
    }

    public void ResetLevels()
    {
        saveData.level1Complete = false;
        saveData.level2Complete = false;
        saveData.level3Complete = false;
        saveData.level4Complete = false;
        saveData.level5Complete = false;
        saveData.level6Complete = false;
        saveData.level7Complete = false;
        saveData.level8Complete = false;
        saveData.level9Complete = false;
        saveData.level10Complete = false;
        saveData.level11Complete = false;
        saveData.level12Complete = false;

        PlayerPrefs.DeleteAll();
        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();

        levelTwoLocked.SetActive(true);
        levelThreeLocked.SetActive(true);
        levelFourLocked.SetActive(true);
        levelFiveLocked.SetActive(true);
        levelSixLocked.SetActive(true);
        levelSevenLocked.SetActive(true);
        levelEightLocked.SetActive(true);
        levelNineLocked.SetActive(true);
        levelTenLocked.SetActive(true);
        levelElevenLocked.SetActive(true);
        levelTwelveLocked.SetActive(true);
    }
    public void LoadLevel1()
    {
        Debug.Log("1");
        SceneManager.LoadScene("Level1");

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }

    public void LoadLevel2()
    {
        Debug.Log("2");
        saveData.level2Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel3()
    {
        Debug.Log("3");
        saveData.level3Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel4()
    {
        Debug.Log("4");
        saveData.level4Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel5()
    {
        Debug.Log("5");
        saveData.level5Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel6()
    {
        Debug.Log("6");
        saveData.level6Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel7()
    {
        Debug.Log("7");
        saveData.level7Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel8()
    {
        Debug.Log("8");
        saveData.level8Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }

    public void LoadLevel9()
    {
        Debug.Log("9");
        saveData.level9Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel10()
    {
        Debug.Log("10");
        saveData.level10Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel11()
    {
        Debug.Log("11");
        saveData.level11Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }
    public void LoadLevel12()
    {
        Debug.Log("12");
        saveData.level12Complete = true;

        saveData.SaveProgress();
        saveData.LoadProgress();
        UpdateLockedLevels();
    }


}
