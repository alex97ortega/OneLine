﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    public LevelManager levelManager;
    Sprite pointerSprite; 

	
    public void SetSprite(Sprite newSprite) { pointerSprite = newSprite; }

    public void TapPointer(float x, float y)
    {
        // si sale el menu de win no mostramos el puntero
        if (levelManager.HasFinished())
            return;
        GetComponent<SpriteRenderer>().sprite = pointerSprite;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        transform.position = new Vector3(x, y, 0);
    }
    public void UntapPointer()
    {
        if (levelManager.HasFinished())
            return;
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }
}
