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
    private static LevelInfo.BlockType[,] ParseLevel (string str)
    {
        // nos aseguramos de que el último caracter no sea un  
        // salto de línea, ya que añadiría una fila de más
        if (str[str.Length-1] == '\n')
            str = str.Remove(str.Length - 1);

        List<List<LevelInfo.BlockType>> blocks = new List<List<LevelInfo.BlockType>>();

        int row = 0;
        int col = 0;

        foreach (char c in str)
        {
            if (c == '\n')
            {
                // el primer caracter siempre va a ser un salto de línea, 
                // por lo que entrará por aquí la primera vuelta del bucle
                //  y añadirá la primera fila del nivel
                blocks.Add(new List<LevelInfo.BlockType>());
                row++;
                col = 0;
            }
            // la 's' marca la casilla de salida
            else if (c == 's')
            {
                blocks[row - 1].Add(LevelInfo.BlockType.FIRST);
                col++;
            }
            // casilla normal
            else if (c == 'o')
            {
                blocks[row - 1].Add(LevelInfo.BlockType.NORMAL);
                col++;
            }
            // casilla vacía
            else if (c == 'x')
            {
                blocks[row - 1].Add(LevelInfo.BlockType.EMPTY);
                col++;
            }
            // no debería haber otro caracter distinto a los anteriores
            // pero si es así, lo ignoro y tirando palante
            else 
            {
                //Debug.Log("Caracter incorrecto en el parseado: " + c);
            }
        }

        LevelInfo.BlockType[,] grid = new LevelInfo.BlockType[row, col];

        // lo paso de lista a matriz (grid)
        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
            {
                grid[i, j] = blocks[i][j];
            }
        return grid;
    }
}
