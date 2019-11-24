using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

    public Sprite pointerSprite;
	
    public void TapPointer(float x, float y)
    {
        GetComponent<Image>().sprite = pointerSprite;
        GetComponent<Image>().color = new Color(255, 255, 255, 255);
        transform.position = new Vector3(x, y, 0);
    }
    public void UntapPointer()
    {
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }
}
