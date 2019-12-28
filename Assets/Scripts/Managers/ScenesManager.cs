using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    // TO MAIN MENU
    public void ChangeToMainMenu()
    {
        ChangeScene("MainMenu");
    }
    public void ChangeToMainMenuWithCoins(int coins)
    {
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
        ChangeScene("SelectLevel");
    }
    public void ChangeToSelectLevel(int difficulty)
    {
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
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            if (gm.GetNextLevelToPass((int)gm.GetCurrentDifficulty()) == gm.GetCurrentLevel())
                Debug.Log("He llegado al tope de niveles de dificultad " + gm.GetCurrentDifficulty().ToString());
            else
            {
                if(gm.RemoveCoins((uint)cost))
                    ChangeScene("Challenge");
            }
        }
        else
            Debug.Log("No hay GameManager!!");
    }
    // OTHERS
    public void Quit()
    {
        Application.Quit();
    }
    void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
