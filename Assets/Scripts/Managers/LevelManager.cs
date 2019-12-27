using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// como existe un ScenesManager encargado de cambiar de escenas, 
// en realidad el levelmanager no tiene apenas funciones, más que activar el menú
//  de fin de nivel e ndicar al gamemanager que ha terminado un nivel para que sume
// su contador de ganados (porque queda feo que esto lo haga el grid)
public class LevelManager : MonoBehaviour {

    public GameObject winMenu, nextLevelButton;
    public Timer challengeTimer;

    public void Win()
    {
        winMenu.SetActive(true);
        // digamos que si existe el timer del modo challenge es que estamos en modo challenge
        if (challengeTimer)
            challengeTimer.Stop();
        // si no, estamos en modo normal
        else
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm)
            {
                bool existeNextLevel = gm.LevelPassed();
                // no activo el botón de siguiente nivel si no hay más niveles de esta dificultad
                if (!existeNextLevel)
                     nextLevelButton.SetActive(false);                
            }
        }        
    }
}
