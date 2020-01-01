using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// sólo para el texto que muestre el número de desafíos logrados
public class Challenges : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            GetComponent<Text>().text = gm.GetNumChalllenges().ToString();
        }
    }	
}
