using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public BlockBehaviour blockPrefab;

    public int filas;
    public int columnas;

    public int posxIni;
    public int posyIni;

    private const int separation = 70;
    private BlockBehaviour[,] grid;

    // necesito una pila que guarde el orden de selección
    private Stack<BlockBehaviour> ordenSeleccionados;

    // Use this for initialization
    void Start () {

        grid = new BlockBehaviour[filas,columnas];

        for(int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                grid[i, j] = Instantiate(blockPrefab);
                grid[i, j].transform.SetParent(gameObject.transform);
                grid[i, j].SetPos(posxIni + (separation * j), posyIni - (separation * i));
                grid[i, j].SetCasilla(i, j);
            }
        }
        grid[0, 0].SetFirst();
        ordenSeleccionados = new Stack<BlockBehaviour>();
        ordenSeleccionados.Push(grid[0, 0]);
	}
	public void CheckFinish()
    {
        foreach(var b in grid)
        {
            if (!b.IsTapped())
                return;
        }
        Debug.Log("Win!");
    }

    // solo puedo seleccionar una nueva casilla si 
    // está pegada a la última que se haya coloreado
    public bool CanBeTapped(BlockBehaviour blockTapped)
    {        
        // compruebo si la nueva casilla es alguna de las que se encuentra 1
        // posición en cualquiera de las 4 direcciones posibles
        BlockBehaviour lastTapped = ordenSeleccionados.Peek();

        int N = lastTapped.GetFila() - 1;
        int S = lastTapped.GetFila() + 1;
        int W = lastTapped.GetColumna() - 1;
        int E = lastTapped.GetColumna() + 1;

        if ((N > -1       && grid[N, lastTapped.GetColumna()] == blockTapped) ||
            (S < filas    && grid[S, lastTapped.GetColumna()] == blockTapped) ||
            (E < columnas && grid[lastTapped.GetFila(),    E] == blockTapped) ||
            (W > -1       && grid[lastTapped.GetFila(),    W] == blockTapped))
        {
            ordenSeleccionados.Push(blockTapped);
            return true;
        }

        return false;
    }

    // solo puedo deseleccionar una casilla si la que estoy seleccionando
    // actualmente es colindante a la última seleccionada que tenemos.
    // y si no es el primero de todos
    public void CheckPosibleUntap(BlockBehaviour blockTapped)
    {
        BlockBehaviour ultimoSelecc = ordenSeleccionados.Peek();

        int N = blockTapped.GetFila() - 1;
        int S = blockTapped.GetFila() + 1;
        int W = blockTapped.GetColumna() - 1;
        int E = blockTapped.GetColumna() + 1;

        if ((N > -1 && grid[N, blockTapped.GetColumna()] == ultimoSelecc) ||
            (S < filas && grid[S, blockTapped.GetColumna()] == ultimoSelecc) ||
            (E < columnas && grid[blockTapped.GetFila(), E] == ultimoSelecc) ||
            (W > -1 && grid[blockTapped.GetFila(), W] == ultimoSelecc))
        {
            if(ultimoSelecc.Untap())
                ordenSeleccionados.Pop();
        }
    }
}
