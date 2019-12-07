﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour {

    public Grid grid;

    // fichero de texto text asset para arrastrar
    public TextAsset[] levels;

    private void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            uint level = FindObjectOfType<GameManager>().GetCurrentLevel();
            GameManager.Difficulty difficulty = FindObjectOfType<GameManager>().GetCurrentDifficulty();
            BuildLevel(difficulty,level);
        }
        else
            Debug.Log("No hay GameManager!!");
    }

    public void BuildLevel(GameManager.Difficulty difficulty,uint levelIndex)
    {
        LevelInfo levelInfo = LevelParser.Parse(levels[(int)difficulty].text,levelIndex);

        //LogLevel(difficulty, levelIndex, levelInfo);

        //creo el grid si he podido paresearlo
        if(levelInfo != null)
            grid.GenerateGrid(levelInfo);
    }

    private void LogLevel(GameManager.Difficulty difficulty, uint level, LevelInfo levelInfo)
    {
        
        Debug.Log("Nivel " + level + " de dificultad " + difficulty);

        Debug.Log("filas: " + levelInfo.Height);
        Debug.Log("columnas: " + levelInfo.Width);

        string aux = "";
        for (int i = 0; i < levelInfo.Height; i++)
        {
            for (int j = 0; j < levelInfo.Width; j++)
            {
                aux += levelInfo.blocks[i, j].ToString();
                aux += " ";
            }

            Debug.Log(aux);
            aux = "";
        }
    }
}
