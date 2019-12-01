using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour {

    public Grid grid;

    // fichero de texto text asset para arrastrar
    public TextAsset[] levels;

    private void Start()
    {
        BuildLevel(0);//quitar
    }
    // this is called by GameManager when the player clicks a levelIndex
    public void BuildLevel(uint levelIndex)
    {
        LevelInfo levelInfo = LevelParser.Parse(levels[levelIndex].text);

        //LogLevel(levelInfo);

        //Build the grid
        grid.GenerateGrid(levelInfo);
    }

    private void LogLevel(LevelInfo levelInfo)
    {
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
