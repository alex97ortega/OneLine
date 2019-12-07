using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomColor : MonoBehaviour {

    public Sprite[] blocks;
    public Sprite[] pointers;

    public void SetRandomColors(out Sprite block, out Sprite pointer)
    {
        if(blocks.Length != pointers.Length)
        {
            Debug.Log("No coincide el número de colores!!");
            block = blocks[0];
            pointer = pointers[0];
        }
        else
        {
            int rdn = Random.Range(0, blocks.Length - 1);
            block = blocks[rdn];
            pointer = pointers[rdn];
        }
    }
}
