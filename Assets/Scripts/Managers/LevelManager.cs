using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// como existe un ScenesManager encargado de cambiar de escenas, 
// en realidad el levelmanager no tiene apenas funciones, más que activar el menú
// de fin de nivel e ndicar al gamemanager que ha terminado un nivel para que sume
// su contador de ganados (porque queda feo que esto lo haga el grid)

// también le he aportado un poco de control sobre las pistas, aunque internamente lo haga todo el grid
public class LevelManager : MonoBehaviour {

    public Grid grid;
    public GameObject winMenu, nextLevelButton;
    public Timer challengeTimer;
    

    // se llama al levantar el dedo de la pantalla y ver que se hayan ocupado todos los bloques
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
    public void Show5Hints(int prize)
    {
        // ni lo intentamos si el usuario no tiene monedas
        GameManager gm = FindObjectOfType<GameManager>();
        if(gm)
        {
            if (gm.GetCoins() < prize)
                return;
        }

        // primero vemos si al menos hay un bloque del recorrido 
        // sin mostrar la pista, ya que en este caso no removemos el dinero
        if (!grid.ShowNextHint())
            return;

        // la anterior llamada ya nos ha marcado 1 pista
        // ya nos da igual que las siguientes 4 existan o no 

        for(int i = 0; i < 4; i++)
        {
            grid.ShowNextHint();
        }
        // siempre se resetea el tablero al comprar una pista
        grid.RestartGrid();

        // remover el dinero debería de hacerse lo primero de todo, antes de  
        // mostrar cualquier pista, pero como tenemos que hacer la comprobación
        // y no influyen el resto de llamadas en el GameManager lo podemos hacer después,
        // el jugador no lo va a a notar
        
        if (gm)
        {
            gm.RemoveCoins((uint)prize);
        }
    }
}
