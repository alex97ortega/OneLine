using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo {

    public enum BlockType
    {
        EMPTY,
        FIRST,
        NORMAL
    }
    private uint width;
    private uint height;

    public BlockType[,] blocks;

    public LevelInfo(uint w, uint h, BlockType[,] bs)
    {
        width =  w;
        height = h;
        blocks = bs;
    }

    public uint Width
    {
        get
        {
            return width;
        }
    }

    public uint Height
    {
        get
        {
            return height;
        }
    }

    public BlockType[,] Blocks
    {
        get
        {
            return blocks;
        }
    }
}
