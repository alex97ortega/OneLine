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
    public int[] maxLevels;
    public LoadManager loadManager;

    uint currentLevel;
    uint coins;
    uint challenges;
    System.DateTime lastChallengeTime;

    struct DifficultyLevelsInfo
    {
        public uint nextLevelToPass;
        public uint maxLevels;
    }

    DifficultyLevelsInfo[] infoDifficulties;

    Difficulty currentDifficulty;

    private void Awake()
    {
        // destruyo el gamemanager si ya hay uno en la escena. 
        // esto me permite cargar distintas escenas que lo necesiten sin tener que ir a la primera

        foreach (var g in FindObjectsOfType<GameManager>())
        {
            if (g.gameObject != gameObject)
                Destroy(gameObject);
        }
    }

    void Start()
    {     
        DontDestroyOnLoad(gameObject);
        infoDifficulties = new DifficultyLevelsInfo[(int)Difficulty.NUM_DIFFICULTIES];
        
        for (int i = 0; i < infoDifficulties.Length; i++)
        {
            infoDifficulties[i].maxLevels = (uint)maxLevels[i];
        }
        // cargamos el progreso
        if(loadManager)
        {
            LoadManager.Data data = loadManager.LoadItems();
            GetGameStateFromLoad(data);
        }
    }

    // DIFICULTIES
    public Difficulty GetCurrentDifficulty() {  return currentDifficulty; }
    public void SetDifficulty(uint dif) { currentDifficulty = (Difficulty)dif; }

    // LEVEL
    public void StartLevel(uint level)
    {
        currentLevel = level;
    }
    public uint GetCurrentLevel() { return currentLevel; }
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
                SaveGameState();
                return true;
            }
            return false;
        }
        return true;
    }
    //CHALLENGES
    public uint GetNumChalllenges() { return challenges; }
    public void ChallengePassed() { challenges++; SaveGameState(); }

    //COINS
    public uint GetCoins() { return coins; }
    public void AddCoins(uint quantity) { coins += quantity; SaveGameState(); }

    // devuelve si ha podido remover la cantidad indicada de monedas, 
    // ya que no se pueden gastar más de las que se tengan en el momento
    public bool RemoveCoins(uint quantity)
    {
        if(quantity <= coins)
        {
            coins -= quantity;
            SaveGameState();
            return true;
        }
        return false;
    }
    //CHALLENGE TIME
    public System.DateTime GetLastChallengeTime() {  return lastChallengeTime; }
    public void SetLastChallengeTime(System.DateTime time) { lastChallengeTime = time; SaveGameState(); }

    //PROGRESO
    private void GetGameStateFromLoad(LoadManager.Data data)
    {
        // set initial coins and challenge data
        coins = data.coins;
        challenges = data.challenges;
        lastChallengeTime = data.lastChallengeTime;

        // set level progress        

        for (int i = 0; i < infoDifficulties.Length; i++)
        {
            infoDifficulties[i].nextLevelToPass = data.nextLevels[i];
        }
    }

    private void SaveGameState()
    {
        uint[] lvls = new uint[infoDifficulties.Length];
        for (int i = 0; i < lvls.Length; i++)
            lvls[i] = infoDifficulties[i].nextLevelToPass;

        loadManager.SaveItems(coins, challenges,lastChallengeTime, lvls);
    }
}
