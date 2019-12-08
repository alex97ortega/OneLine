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
    uint currentLevel;

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
        currentLevel = level;
    }
    public uint GetCurrentLevel() { return currentLevel; }

    public Difficulty GetCurrentDifficulty() {  return currentDifficulty; }
    public void SetDifficulty(uint dif) { currentDifficulty = (Difficulty)dif; }

    public uint GetNextLevelToPass(int difficulty) { return infoDifficulties[difficulty].nextLevelToPass; }
    public uint GetMaxLevels(int difficulty) { return infoDifficulties[difficulty].maxLevels; }

    // devuelve true si existe un nivel posterior al actual
    public bool LevelPassed() {
        int dif = (int)currentDifficulty;
        // primero hay que comprobar que el nivel que se ha jugado coincide con el último
        // de la dificultad actual, para poder desbloquear el siguiente
        if(currentLevel == infoDifficulties[dif].nextLevelToPass)
        {
            // luego comprobamos que no es el último nivel pasable de esa dificultad
            if (infoDifficulties[dif].nextLevelToPass < infoDifficulties[dif].maxLevels)
            {
                infoDifficulties[dif].nextLevelToPass++;
                return true;
            }
            return false;
        }
        return true;
    }
}
