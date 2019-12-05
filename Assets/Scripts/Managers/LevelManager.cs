using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Grid grid;

	// Use this for initialization
	void Start () {
		
	}
	public void RestartLevel()
    {
        grid.RestartGrid();
    }
    public void Back()
    {
        FindObjectOfType<GameManager>().ChangeScene("SelectLevel");
    }
}
