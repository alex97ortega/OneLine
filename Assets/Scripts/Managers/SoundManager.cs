using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonSound()
    {
        PlaySound("button");
    }
    public void PlayTapSound()
    {
        PlaySound("blockTap");
    }
    public void PlayWinSound()
    {
        PlaySound("win");
    }
    public void PlayRestartSound()
    {
        // para que no haga el sonido de restart si ya está haciendo el de hint
        foreach (var x in GetComponents<AudioSource>())
        {
            if (x.clip.name == "hint" && x.isPlaying)
                return;
        }
        PlaySound("restart");
    }
    public void PlayHintSound()
    {
        PlaySound("hint");
    }
    public void PlayChallengeButtonSound()
    {
        PlaySound("challengeButton");
    }
    public void PlayWinChallengeSound()
    {
        // para que pare de reproducir el sonido del timer
        foreach (var x in GetComponents<AudioSource>())
        {
            if (x.clip.name == "timer" && x.isPlaying)
                Debug.Log("para");
        }
        PlaySound("winChallenge");
    }
    public void PlayLoseChallengeSound()
    {
        // para que pare de reproducir el sonido del timer
        foreach (var x in GetComponents<AudioSource>())
        {
            if (x.clip.name == "timer" && x.isPlaying)
                x.Stop();
        }
        PlaySound("loseChallenge");
    }
    public void PlayTimerSound()
    {
        foreach (var x in GetComponents<AudioSource>())
        {
            if (x.clip.name == "timer" && !x.isPlaying)
            {
                x.Play();
            }
        }
    }
    public void PlaySound(string sound)
    {
        foreach (var x in GetComponents<AudioSource>())
        {
            if (x.clip.name == sound)
                x.Play();
        }
    }
}
