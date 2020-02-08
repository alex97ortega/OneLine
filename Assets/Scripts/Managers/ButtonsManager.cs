using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// este método va a crear los botones que sean necesarios
// para cada nivel en el selector de niveles
public class ButtonsManager : MonoBehaviour {

    public SelectLevelButton[] buttons;

	// Use this for initialization
	void Start () {
        GameManager gm = FindObjectOfType<GameManager>();
        
        uint numBotones = gm.GetMaxLevels((int)gm.GetCurrentDifficulty());

        for(uint i =0; i < buttons.Length; i++)
        {
            buttons[i].SetLevel(i + 1);
            // se muestran sólo el número de niveles que haya
            if (i > numBotones-1)
                buttons[i].gameObject.SetActive(false);

            else if (i < gm.GetNextLevelToPass((int)gm.GetCurrentDifficulty()))
                buttons[i].Unlock(); // lo desbloqueo
        }		
	}	
}
