using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SetRandomColor))]

public class Grid : MonoBehaviour {

    public LevelManager levelManager;

    public BlockBehaviour blockPrefab;
    public Pointer pointer;
    
    public float separation;
    private BlockBehaviour[,] tablero;

    // esta variable está solo para que no se puedan
    // quitar bloques marcados al acabar un nivel
    bool win = false;

    uint filas;
    uint columnas;

    float posxIni;
    float posyIni;

    // necesito una pila que guarde el orden de selección
    private Stack<BlockBehaviour> ordenSeleccionados;
    
    public void GenerateGrid(LevelInfo levelInfo)
    {
        // primero inicializo un color aleatorio del tablero
        Sprite rdnBlock, rdnPointer;
        GetComponent<SetRandomColor>().SetRandomColors(out rdnBlock, out rdnPointer);

        pointer.SetSprite(rdnPointer);

        filas = levelInfo.Height;
        columnas = levelInfo.Width;
        tablero = new BlockBehaviour[filas, columnas];
        ordenSeleccionados = new Stack<BlockBehaviour>();

        posxIni = (columnas-1) * (-separation/2);
        posyIni = (filas-1) * (separation/2);
        // creo el grid
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if(levelInfo.blocks[i,j] != LevelInfo.BlockType.EMPTY)
                {
                    tablero[i, j] = Instantiate(blockPrefab);
                    tablero[i, j].transform.SetParent(gameObject.transform);
                    tablero[i, j].SetPos(posxIni + (separation * j), posyIni - (separation * i));
                    tablero[i, j].SetCasilla(i, j);
                    tablero[i, j].SetSprite(rdnBlock);

                    if(levelInfo.blocks[i, j] == LevelInfo.BlockType.FIRST)
                    {
                        if(ordenSeleccionados.Count != 0)
                        {
                            Debug.Log("No puede haber 2 primeras casillas!!");
                            return;
                        }
                        tablero[i, j].SetFirst();
                        ordenSeleccionados.Push(tablero[i, j]);
                    }
                }
            }
        }
        // hacemos el grid más pequeño para cuando el tablero sea de 7 filas o más
        // para que no se salga de los bordes
        if (filas == 7) //EXPERT
            transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
        else if (filas == 8) //MASTER
            transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
    }

    // sólo se llama aquí para ver si se ha acabado la partida
    // cuando se levante el dedo de la pantalla
    public void CheckFinish()
    {
        foreach(var b in tablero)
        {
            if (b && !b.IsTapped())
                return;
        }
        win = true;
        levelManager.Win();
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
                ordenSeleccionados.Peek().DisactiveLastDir();
            }
        }     
    }

    public void OnTouchDown(float x, float y)
    {
        foreach (var b in tablero)
        {
            if (b && b.Inside(x,y))
            {
                b.Tap();
                return;
            }
        }
    }
    public void RestartGrid()
    {
        while (ordenSeleccionados.Count != 1)
        {
            ordenSeleccionados.Peek().Untap();
            ordenSeleccionados.Pop();
            ordenSeleccionados.Peek().DisactiveLastDir();
        }
    }
}
