using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            GetComponent<Text>().text = gm.GetCoins().ToString();
        }
    }	
}
