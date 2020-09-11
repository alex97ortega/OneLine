using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLogo : MonoBehaviour {

    float cont;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().color= new Vector4(1, 1, 1, 0);
    }
	
	// Update is called once per frame
	void Update () {
        cont += Time.deltaTime/2;
        GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, cont);
        if (cont > 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
