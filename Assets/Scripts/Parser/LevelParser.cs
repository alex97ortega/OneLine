using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParser {

    public static LevelInfo Parse(string file)
    {
        var fileParsed = ParseFile(file);

        uint width = (uint)fileParsed.GetLength(1);
        uint height = (uint)fileParsed.GetLength(0);
        
        LevelInfo.BlockType[,] blockInfo = new LevelInfo.BlockType[height,width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                blockInfo[i, j] = fileParsed[i, j];

        LevelInfo levelInfo = new LevelInfo(width, height, blockInfo);

        return levelInfo;
    }

    private static LevelInfo.BlockType[,] ParseFile (string str)
    {
        string aux = "";

        List<List<LevelInfo.BlockType>> blocks = new List<List<LevelInfo.BlockType>>();

        int row = 0;
        int col = 0;
        blocks.Add(new List<LevelInfo.BlockType>());
        row++;

        foreach (char c in str)
        {
            if (c == '\n')
            {
                blocks.Add(new List<LevelInfo.BlockType>());
                row++;
                col = 0;
            }
            else
            {
                aux += c;
                int i;
                int.TryParse(aux, out i);
                blocks[row-1].Add((LevelInfo.BlockType)i);
                aux = "";
                col++;
            }
        }

        LevelInfo.BlockType[,] grid = new LevelInfo.BlockType[row, col];

        for (int i = 0; i < row; i++)
            for (int j = 0; j < col; j++)
            {
                grid[i, j] = blocks[i][j];
            }
        return grid;
    }
}
