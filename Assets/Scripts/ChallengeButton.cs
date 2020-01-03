using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script que controla si se puede o no pulsar el botón de challenge
public class ChallengeButton : MonoBehaviour
{
    public GameObject challengeMenu, challengeLocked;
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if (gm.GetLastChallengeTime() != System.DateTime.MinValue)
                challengeLocked.SetActive(true);
        }
    }
    public void OnPress()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if (!challengeLocked.activeSelf)
                challengeMenu.SetActive(true);
        }
    }
}
