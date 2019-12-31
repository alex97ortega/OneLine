using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParser {

    public static LevelInfo Parse(string file, uint level)
    {
        // primero troceamos el fichero para quedarnos con el nivel que necesitamos

        // con esto nos quitamos todo lo anterior del fichero hasta llegar a nuestro nivel
        string[] splited = file.Split(new string[]{ level.ToString() }, System.StringSplitOptions.None);
        
        if(splited.Length == 1)
        {
            Debug.Log("No encontrado nivel " + level);
            return null;
        }
        level++;
        // y con esto nos quitamos todo lo posterior
        string[] splitedFinal = splited[1].Split(new string[] { level.ToString() }, System.StringSplitOptions.None);
        
        // ya podemos parsear el nivel
        var fileParsed = ParseLevel(splitedFinal[0]);
        uint width = (uint)fileParsed.GetLength(1);
        uint height = (uint)fileParsed.GetLength(0);
        
        LevelInfo levelInfo = new LevelInfo(width, height, fileParsed);

        return levelInfo;
    }

    // método que parsea un string que represente un nivel
    private static LevelInfo.BlockInfo[,] ParseLevel (string str)
    {
        // nos aseguramos de que el último caracter no sea un  
        // salto de línea, ya que añadiría una fila de más
        if (str[str.Length-1] == '\n')
            str = str.Remove(str.Length - 1);

        List<List<LevelInfo.BlockInfo>> blocks = new List<List<LevelInfo.BlockInfo>>();

        int row = 0;
        int col = 0;

        LevelInfo.BlockInfo aux;

        foreach (char c in str)
        {
            if (c == '\n')
            {
                // el primer caracter siempre va a ser un salto de línea, 
                // por lo que entrará por aquí la primera vuelta del bucle
                //  y añadirá la primera fila del nivel
                blocks.Add(new List<LevelInfo.BlockInfo>());
                row++;
                col = 0;
            }
            // la 'A' marca la casilla de salida
            else if (c == 'A')
            {
                aux.type = LevelInfo.BlockType.FIRST;
                aux.order = c;
                blocks[row - 1].Add(aux);
                col++;
            }
            // casilla vacía es un espacio
            else if (c == ' ')
            {
                aux.type = LevelInfo.BlockType.EMPTY;
                aux.order = c;
                blocks[row - 1].Add(aux);
                col++;
            }
            // el resto de letras representa el camino y son casillas normales.
            // no se qué narices es el caracter 13, me sale como si fuera
            // un espacio en blanco pero eso ya lo trato arriba, y me 
            // estropea la conversión si no hago esto
            else if (c != 13)
            {
                aux.type = LevelInfo.BlockType.NORMAL;
                aux.order = c;
                blocks[row - 1].Add(aux);
                col++;
            }
        }

        LevelInfo.BlockInfo[,] grid = new LevelInfo.BlockInfo[row, col];

        // lo paso de lista a matriz (grid)
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
            {
                grid[i, j] = blocks[i][j];
            }
        return grid;
    }
}
