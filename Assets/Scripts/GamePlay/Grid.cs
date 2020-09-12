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
    // quitar o marcar bloques al acabar un nivel
    bool finish = false;

    uint filas;
    uint columnas;

    float posxIni;
    float posyIni;
    Vector3 initialScale;

    // necesito una pila que guarde el orden de selección
    private Stack<BlockBehaviour> ordenSeleccionados;

    // y también un último bloque mostrado como pista
    private BlockBehaviour lastHintBlock;
    
    public void GenerateGrid(LevelInfo levelInfo)
    {
        // primero inicializo un color aleatorio del tablero
        Sprite rdnBlock, rdnPointer, rdnHint;
        GetComponent<SetRandomColor>().SetRandomColors(out rdnBlock, out rdnPointer, out rdnHint);

        pointer.SetSprite(rdnPointer);

        filas = levelInfo.Height;
        columnas = levelInfo.Width;
        tablero = new BlockBehaviour[filas, columnas];
        ordenSeleccionados = new Stack<BlockBehaviour>();

        posxIni = (columnas-1) * (-separation/2);
        posyIni = (filas-1) * (separation/2)-0.2f;
        // creo el grid
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if(levelInfo.blocks[i,j].type != LevelInfo.BlockType.EMPTY)
                {
                    tablero[i, j] = Instantiate(blockPrefab);
                    tablero[i, j].transform.SetParent(gameObject.transform);
                    tablero[i, j].SetPos(posxIni + (separation * j), posyIni - (separation * i));
                    tablero[i, j].SetCasilla(i, j);
                    tablero[i, j].SetSprite(rdnBlock);
                    //pistas
                    tablero[i, j].SetHintSprite(rdnHint);
                    tablero[i, j].SetOrdenHint(levelInfo.blocks[i, j].order);

                    if(levelInfo.blocks[i, j].type == LevelInfo.BlockType.FIRST)
                    {
                        if(ordenSeleccionados.Count != 0)
                        {
                            Debug.Log("No puede haber 2 primeras casillas!!");
                            return;
                        }
                        tablero[i, j].SetFirst();
                        ordenSeleccionados.Push(tablero[i, j]);
                        // la casilla primera no cuenta como pista para dar
                        lastHintBlock = tablero[i, j];
                    }
                }
            }
        }
        // hacemos el grid más pequeño independientemente de la resolución para 
        // que no roce los bordes laterales
        
        transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);

        initialScale = transform.localScale;
    }
    private void Update()
    {
        // hacer grid más pequeño si no hay espacio de pixeles por la resolución
        // hasta una resolución de aspecto de 1.667 no hace falta tocar nada
        float relation = (float)Screen.height / (float)Screen.width;
        float relationRef = 800.0f / 480.0f;
        if (filas > 3 && relation < relationRef)
        {
            if(filas < 6)
                transform.localScale = new Vector3(1 - (relationRef - relation) + 0.1f * filas, 1 - (relationRef - relation) + 0.1f * filas, 1.0f);
            else
                transform.localScale = new Vector3(1 - (relationRef - relation), 1 - (relationRef - relation), 1.0f);
        }
        else
            transform.localScale = initialScale;

    }
    // sólo se llama aquí para ver si se ha acabado la partida
    // cuando se levante el dedo de la pantalla
    public void CheckFinish()
    {
        if (finish)
            return;

        foreach(var b in tablero)
        {
            if (b && !b.IsTapped())
                return;
        }
        finish = true;
        levelManager.Win();
    }

    // solo puedo seleccionar una nueva casilla si 
    // está pegada a la última que se haya coloreado
    public bool CanBeTapped(BlockBehaviour blockTapped)
    {
        if (finish)
            return false;
        // compruebo si la nueva casilla es alguna de las que se encuentra 1
        // posición en cualquiera de las 4 direcciones posibles
        BlockBehaviour lastTapped = ordenSeleccionados.Peek();

        int N = lastTapped.GetFila() - 1;
        int S = lastTapped.GetFila() + 1;
        int W = lastTapped.GetColumna() - 1;
        int E = lastTapped.GetColumna() + 1;

        if (N > -1       && tablero[N, lastTapped.GetColumna()] == blockTapped)
        {
            lastTapped.ActivateDirItem(BlockBehaviour.Dirs.up);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (S < filas    && tablero[S, lastTapped.GetColumna()] == blockTapped)
        {
            lastTapped.ActivateDirItem(BlockBehaviour.Dirs.down);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (E < columnas && tablero[lastTapped.GetFila(),    E] == blockTapped)
        {
            lastTapped.ActivateDirItem(BlockBehaviour.Dirs.right);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }
        if  (W > -1       && tablero[lastTapped.GetFila(),    W] == blockTapped)
        {
            lastTapped.ActivateDirItem(BlockBehaviour.Dirs.left);
            ordenSeleccionados.Push(blockTapped);
            return true;
        }

        return false;
    }

    // deselecciono todo el camino recorrido hasta la casilla 
    // que estamos seleccionando actualmente
    public void UntapOlders(BlockBehaviour blockTapped)
    {
        if (!finish)
        {
            while (ordenSeleccionados.Peek() != blockTapped)
            {
                ordenSeleccionados.Peek().Untap();
                ordenSeleccionados.Pop();
                ordenSeleccionados.Peek().DisactivateDirItem();
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
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm)
            sm.PlayRestartSound();
        while (ordenSeleccionados.Count != 1)
        {
            ordenSeleccionados.Peek().Untap();
            ordenSeleccionados.Pop();
            ordenSeleccionados.Peek().DisactivateDirItem();
        }
    }

    // devuelve true si ha encontrado un bloque con pista para dar
    public bool ShowNextHint()
    {
        int lastHint = lastHintBlock.GetOrdenHint();

        int N = lastHintBlock.GetFila() - 1;
        int S = lastHintBlock.GetFila() + 1;
        int W = lastHintBlock.GetColumna() - 1;
        int E = lastHintBlock.GetColumna() + 1;

        // tiene que ser justo el bloque siguiente al último marcado como pista
        // por narices (si existe) tiene que ser adyacente
        if (N > -1 && tablero[N, lastHintBlock.GetColumna()] &&
            tablero[N, lastHintBlock.GetColumna()].GetOrdenHint() == lastHint + 1)
        {
            lastHintBlock.ActivateHint(BlockBehaviour.Dirs.up);
            lastHintBlock = tablero[N, lastHintBlock.GetColumna()];
            return true;
        }
        if (S < filas && tablero[S, lastHintBlock.GetColumna()] 
            && tablero[S, lastHintBlock.GetColumna()].GetOrdenHint() == lastHint + 1)
        {
            lastHintBlock.ActivateHint(BlockBehaviour.Dirs.down);
            lastHintBlock = tablero[S, lastHintBlock.GetColumna()];
            return true;
        }
        if (E < columnas && tablero[lastHintBlock.GetFila(), E] 
            && tablero[lastHintBlock.GetFila(), E].GetOrdenHint() == lastHint + 1)
        {
            lastHintBlock.ActivateHint(BlockBehaviour.Dirs.right);
            lastHintBlock = tablero[lastHintBlock.GetFila(), E];
            return true;
        }
        if (W > -1 && tablero[lastHintBlock.GetFila(), W] 
            && tablero[lastHintBlock.GetFila(), W].GetOrdenHint() == lastHint + 1)
        {
            lastHintBlock.ActivateHint(BlockBehaviour.Dirs.left);
            lastHintBlock = tablero[lastHintBlock.GetFila(), W];
            return true;
        }
        return false;
    }
    public void Finish() { finish = true; }
    public bool HasFinished() { return finish; }
}
