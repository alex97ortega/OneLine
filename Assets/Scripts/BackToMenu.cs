using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void BackMainMenu()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
            gm.ChangeScene("MainMenu");
        else
            Debug.Log("No hay GameManager!!");
    }
    public void BackSelectLevel()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
            gm.ChangeScene("SelectLevel");
        else
            Debug.Log("No hay GameManager!!");
    }
}
