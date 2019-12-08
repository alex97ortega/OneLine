using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// como existe un ScenesManager encargado de cambiar de escenas, 
// en realidad el levelmanager no tiene apenas funciones, más que activar el menú
//  de fin de nivel e ndicar al gamemanager que ha terminado un nivel para que sume
// su contador de ganados (porque queda feo que esto lo haga el grid)
public class LevelManager : MonoBehaviour {

    public GameObject winMenu, nextLevelButton;

    public void Win()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm)
        {
            bool existeNextLevel = gm.LevelPassed();
            winMenu.SetActive(true);
            // no activo el botón de siguiente nivel si no hay más niveles de esta dificultad
            if(!existeNextLevel)
                nextLevelButton.SetActive(false);
        }
    }
}
