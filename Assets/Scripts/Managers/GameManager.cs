using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    uint curentLevel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void StartLevel(uint level)//falta la dificultad
    {
        curentLevel = level;
        ChangeScene("GamePlay");
    }
    public uint GetCurrentLevel() { return curentLevel; }
}
