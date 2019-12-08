using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Reutilizamos este script para mostrar info de los niveles y de la dificultad
// tanto en gameplay como en el menú principal, ya que prácticamente el texto es el mismo
public class InfoLevelText : MonoBehaviour {

    public int difficulty;

	// Use this for initialization
	void Start () {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm)
        {
            // si le pasamos -2 muestro sólo la dificultad actual (SelectLevel)
            if (difficulty == -2)
                GetComponent<Text>().text = gm.GetCurrentDifficulty().ToString();

            // si le pasamos -1 muestro la dificultad + el nivel actual (GamePlay)
            else if (difficulty == -1)
                GetComponent<Text>().text = gm.GetCurrentDifficulty().ToString() + "  " +
                                            gm.GetCurrentLevel().ToString();

            // si no, muestro la dificultad que marque y los niveles obtenidos de esa dificultad (MainMenu)
            else
                GetComponent<Text>().text = gm.GetNextLevelToPass(difficulty).ToString() + "/" +
                                             gm.GetMaxLevels(difficulty);            
        }
    }	
}
