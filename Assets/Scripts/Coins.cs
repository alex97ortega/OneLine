using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

    GameManager gm;
    uint coins;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            coins = gm.GetCoins();
            GetComponent<Text>().text = coins.ToString();
        }
    }
    private void Update()
    {
        if (gm)
        {
            // para que actualice las monedas
            if(coins != gm.GetCoins())
            {
                coins = gm.GetCoins();
                GetComponent<Text>().text = coins.ToString();
            }
        }
    }
}
