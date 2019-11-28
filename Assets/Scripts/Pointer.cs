using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    public Sprite pointerSprite; // no va a ser public
	
    public void SetSprite(Sprite newSprite) { pointerSprite = newSprite; }

    public void TapPointer(float x, float y)
    {
        float realX = x / 108.0f;
        float realY = y / 192.0f;

        GetComponent<SpriteRenderer>().sprite = pointerSprite;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        transform.position = new Vector3(realX-1, realY+1, 0);
    }
    public void UntapPointer()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }
}
