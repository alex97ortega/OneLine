using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// da una piel, puntero de touch y marca de camino de un mismo color aleatorio
public class SetRandomColor : MonoBehaviour {

    public Sprite[] blocks;
    public Sprite[] pointers;
    public Sprite[] hints;

    public void SetRandomColors(out Sprite block, out Sprite pointer, out Sprite hint)
    {
        if((blocks.Length != pointers.Length) || (blocks.Length != hints.Length))
        {
            Debug.Log("No coincide el número de colores!!");
            block = blocks[0];
            pointer = pointers[0];
            hint = hints[0];
        }
        else
        {
            int rdn = Random.Range(0, blocks.Length);
            block = blocks[rdn];
            pointer = pointers[rdn];
            hint = hints[rdn];
        }
    }
}
