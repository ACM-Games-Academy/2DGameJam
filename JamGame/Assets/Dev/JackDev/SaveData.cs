using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public bool level1Complete = false;
    public bool level2Complete = false;
    public bool level3Complete = false;
    public bool level4Complete = false;
    public bool level5Complete = false;
    public bool level6Complete = false;
    public bool level7Complete = false;
    public bool level8Complete = false;
    public bool level9Complete = false;
    public bool level10Complete = false;
    public bool level11Complete = false;
    public bool level12Complete = false;

    // Save the data to PlayerPrefs
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("Level1Complete", level1Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level2Complete", level2Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level3Complete", level3Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level4Complete", level4Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level5Complete", level5Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level6Complete", level6Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level7Complete", level7Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level8Complete", level8Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level9Complete", level9Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level10Complete", level10Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level11Complete", level11Complete ? 1 : 0);
        PlayerPrefs.SetInt("Level12Complete", level12Complete ? 1 : 0);

    }

    // Load the data from PlayerPrefs
    public void LoadProgress()
    {
        level1Complete = PlayerPrefs.GetInt("Level1Complete", 0) == 1;
        level2Complete = PlayerPrefs.GetInt("Level2Complete", 0) == 1;
        level3Complete = PlayerPrefs.GetInt("Level3Complete", 0) == 1;
        level4Complete = PlayerPrefs.GetInt("Level4Complete", 0) == 1;
        level5Complete = PlayerPrefs.GetInt("Level5Complete", 0) == 1;
        level6Complete = PlayerPrefs.GetInt("Level6Complete", 0) == 1;
        level7Complete = PlayerPrefs.GetInt("Level7Complete", 0) == 1;
        level8Complete = PlayerPrefs.GetInt("Level8Complete", 0) == 1;
        level9Complete = PlayerPrefs.GetInt("Level9Complete", 0) == 1;
        level10Complete = PlayerPrefs.GetInt("Level10Complete", 0) == 1;
        level11Complete = PlayerPrefs.GetInt("Level11Complete", 0) == 1;
        level12Complete = PlayerPrefs.GetInt("Level12Complete", 0) == 1;

    }

    private void Start()
    {
        LoadProgress();
  
    }
}
