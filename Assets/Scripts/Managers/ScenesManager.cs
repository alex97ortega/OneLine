using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

    public void ChangeToMainMenu()
    {
        ChangeScene("MainMenu");
    }
    public void ChangeToSelectLevel(int difficulty)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            // si pasamos -1 significa que no hace falta cambiar la dificultad
            if (difficulty == -1)
                difficulty = (int)gm.GetCurrentDifficulty();
            gm.SetDifficulty((uint)difficulty);
            ChangeScene("SelectLevel");
        }
        else
            Debug.Log("No hay GameManager!!");
    }
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
    void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
