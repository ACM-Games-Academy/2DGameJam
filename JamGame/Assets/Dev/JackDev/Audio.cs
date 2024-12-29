using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip searching;
    public AudioClip cameraSearch;
    public AudioClip buttonPress;
    public AudioClip lose;
    public AudioClip win;

    public void PlaySearchingSound()
    {
        PlaySound(searching);
    }

    public void PlayCameraSearchingSound()
    {
        PlaySound(cameraSearch);
    }

    public void PlayButtonPressSound()
    {
        PlaySound(buttonPress);
    }

    public void PlayLoseSound()
    {
        PlaySound(lose);
    }

    public void PlayWinSound()
    {
        PlaySound(win);
    }

    private void PlaySound(AudioClip clip)
    {
        if (!audioSource.isPlaying || audioSource.clip != clip) 
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
