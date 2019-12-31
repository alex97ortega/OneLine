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

    public struct BlockInfo
    {
        public BlockType type;
        public char order;
    }
    public BlockInfo[,] blocks;

    public LevelInfo(uint w, uint h, BlockInfo[,] bs)
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

    public BlockInfo[,] Blocks
    {
        get
        {
            return blocks;
        }
    }
}
