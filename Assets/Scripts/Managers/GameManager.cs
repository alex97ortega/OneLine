using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public enum Difficulty
    {
        BEGINNER,
        REGULAR,
        ADVANCED,
        EXPERT,
        MASTER
    }
    uint curentLevel;
    Difficulty currentDifficulty;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void StartLevel(uint level)
    {
        curentLevel = level;
    }
    public uint GetCurrentLevel() { return curentLevel; }

    public Difficulty GetCurrentDifficulty() {  return currentDifficulty; }
    public void SetDifficulty(uint dif) { currentDifficulty = (Difficulty)dif; }
}
