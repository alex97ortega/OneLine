using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetRandomColor))]

public class Grid : MonoBehaviour {

    public BlockBehaviour blockPrefab;
    public Pointer pointer;

    public int filas;
    public int columnas;

    public int posxIni;
    public int posyIni;

    private const int separation = 70;
    private BlockBehaviour[,] tablero;

    //provisional hasta que haya un levelmanager
    bool win = false;

    // necesito una pila que guarde el orden de selección
    private Stack<BlockBehaviour> ordenSeleccionados;

    // Use this for initialization
    void Start () {

        // primero inicializo un color aleatorio del tablero
        Sprite rdnBlock, rdnPointer;
        GetComponent<SetRandomColor>().SetRandomColors(out rdnBlock, out rdnPointer);

        pointer.SetSprite(rdnPointer);

        tablero = new BlockBehaviour[filas,columnas];

        // creo el grid
        for(int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                tablero[i, j] = Instantiate(blockPrefab);
                tablero[i, j].transform.SetParent(gameObject.transform);
                tablero[i, j].SetPos(posxIni + (separation * j), posyIni - (separation * i));
                tablero[i, j].SetCasilla(i, j);
                tablero[i, j].SetSprite(rdnBlock);  
            }
        }

        tablero[0, 0].SetFirst();
        ordenSeleccionados = new Stack<BlockBehaviour>();
        ordenSeleccionados.Push(tablero[0, 0]);
	}
    // ahora este método se llama cada vez que se selecciona una nueva casilla
    // pero en el juego no es así, sólo se llama aquí cuando se levante el dedo
    // de la pantalla
	public void CheckFinish()
    {
        foreach(var b in tablero)
        {
            if (b && !b.IsTapped())
                return;
        }
        Debug.Log("Win !");
        win = true;
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

        if (N > -1       && tablero[N, lastTapped.GetColumna()] == blockTapped)
        {
            lastTapped.ActiveDirItem(BlockBehaviour.Dirs.up);
            blockTapped.ActiveDirItem(BlockBehaviour.Dirs.down);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (S < filas    && tablero[S, lastTapped.GetColumna()] == blockTapped)
        {
            lastTapped.ActiveDirItem(BlockBehaviour.Dirs.down);
            blockTapped.ActiveDirItem(BlockBehaviour.Dirs.up);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (E < columnas && tablero[lastTapped.GetFila(),    E] == blockTapped)
        {
            lastTapped.ActiveDirItem(BlockBehaviour.Dirs.right);
            blockTapped.ActiveDirItem(BlockBehaviour.Dirs.left);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (W > -1       && tablero[lastTapped.GetFila(),    W] == blockTapped)
        {
            lastTapped.ActiveDirItem(BlockBehaviour.Dirs.left);
            blockTapped.ActiveDirItem(BlockBehaviour.Dirs.right);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }

        return false;
    }

    // deselecciono todo el camino recorrido hasta la casilla 
    // que estamos seleccionando actualmente
    public void UntapOlders(BlockBehaviour blockTapped)
    {
        if (!win)
        {
            while (ordenSeleccionados.Peek() != blockTapped)
            {
                ordenSeleccionados.Peek().Untap();
                ordenSeleccionados.Pop();
            }
        }
        ordenSeleccionados.Peek().DisactiveLastDir();
    }
}
