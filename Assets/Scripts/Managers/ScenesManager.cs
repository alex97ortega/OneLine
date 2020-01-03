using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    // TO MAIN MENU
    public void ChangeToMainMenu()
    {
        PlayButtonSound();
        ChangeScene("MainMenu");
    }
    public void ChangeToMainMenuWithCoins(int coins)
    {
        PlayButtonSound();
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            gm.AddCoins((uint)coins);
        }
        else
            Debug.Log("No hay GameManager!!");
        ChangeScene("MainMenu");
    }
    // TO SELECT LEVEL
    public void ChangeToSelectLevel()
    {
        PlayButtonSound();
        ChangeScene("SelectLevel");
    }
    public void ChangeToSelectLevel(int difficulty)
    {
        PlayButtonSound();
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            gm.SetDifficulty((uint)difficulty);
            ChangeScene("SelectLevel");
        }
        else
            Debug.Log("No hay GameManager!!");
    }
    // TO GAMEPLAY
    public void ChangeToGamePlay(uint level)
    {
        PlayButtonSound();
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            gm.StartLevel(level);
            ChangeScene("GamePlay");
        }
        else
            Debug.Log("No hay GameManager!!");
    }
    public void ChangeToGamePlayNextLevel()
    {
        PlayButtonSound();
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if (gm.GetNextLevelToPass((int)gm.GetCurrentDifficulty()) == gm.GetCurrentLevel())
                Debug.Log("He llegado al tope de niveles de dificultad " + gm.GetCurrentDifficulty().ToString());
            else
            {
                gm.StartLevel(gm.GetCurrentLevel() + 1);
                ChangeScene("GamePlay");
            }
        }
        else
            Debug.Log("No hay GameManager!!");
    }
    // TO CHALLENGE
    public void ChangeToChallenge(int cost)
    {
        PlayButtonSound();
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if(gm.RemoveCoins((uint)cost))
               ChangeScene("Challenge");
        }
        else
            Debug.Log("No hay GameManager!!");
    }
    // OTHERS
    public void Quit()
    {
        PlayButtonSound();
        Application.Quit();
    }
    void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    public void PlayButtonSound()
    {
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm)
            sm.PlayButtonSound();
    }
}
