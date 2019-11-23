using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

    public Sprite pointerSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            GetComponent<Image>().sprite = pointerSprite;
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().color = new Color(255, 255, 255,0);
        }
    }
}
