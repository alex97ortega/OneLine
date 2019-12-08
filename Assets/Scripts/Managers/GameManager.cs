using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// gestor global con todas las variables y parámetros que necesitan ser guardados

public class GameManager : MonoBehaviour
{    
    public enum Difficulty
    {
        BEGINNER,
        REGULAR,
        ADVANCED,
        EXPERT,
        MASTER, 
        NUM_DIFFICULTIES
    }
    uint curentLevel;

    struct DifficultyLevelsInfo
    {
        public uint nextLevelToPass;
        public uint maxLevels;
    }

    DifficultyLevelsInfo[] infoDifficulties;

    Difficulty currentDifficulty;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        infoDifficulties = new DifficultyLevelsInfo[(int)Difficulty.NUM_DIFFICULTIES];

        for (int i = 0; i < infoDifficulties.Length; i++)
        {
            infoDifficulties[i].nextLevelToPass = 1;
            infoDifficulties[i].maxLevels = 10;
        }
    }
    
    public void StartLevel(uint level)
    {
        curentLevel = level;
    }
    public uint GetCurrentLevel() { return curentLevel; }

    public Difficulty GetCurrentDifficulty() {  return currentDifficulty; }
    public void SetDifficulty(uint dif) { currentDifficulty = (Difficulty)dif; }

    public uint GetNextLevelToPass(int difficulty) { return infoDifficulties[difficulty].nextLevelToPass; }
    public uint GetMaxLevels(int difficulty) { return infoDifficulties[difficulty].maxLevels; }
}
