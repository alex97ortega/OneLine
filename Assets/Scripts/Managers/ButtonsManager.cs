using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// este método va a crear los botones que sean necesarios
// para cada nivel en el selector de niveles
public class ButtonsManager : MonoBehaviour {

    public SelectLevelButton buttonPrefab;

    
	// Use this for initialization
	void Start () {
        GameManager gm = FindObjectOfType<GameManager>();

        Vector3 initialpos = new Vector3(-300, 500, 0);
        uint numBotones = gm.GetMaxLevels((int)gm.GetCurrentDifficulty());

        for(uint i =0; i < numBotones; i++)
        {
            SelectLevelButton newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(gameObject.transform);
            newButton.transform.localPosition = initialpos;
            newButton.transform.localPosition += new Vector3(150 * (i % 5), -150 * (i / 5), 0);
            newButton.SetLevel(i + 1);

            if (i < gm.GetNextLevelToPass((int)gm.GetCurrentDifficulty()))
                newButton.Unlock(); // lo desbloqueo
        }		
	}	
}
