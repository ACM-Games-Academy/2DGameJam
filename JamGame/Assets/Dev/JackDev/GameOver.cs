using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Combat combat;
    private GameObject fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut = GameObject.Find("FadeOut");
    }

    // Update is called once per frame
   /* void Update()
    {
        if (combat.isDetected)
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
    }*/
}
