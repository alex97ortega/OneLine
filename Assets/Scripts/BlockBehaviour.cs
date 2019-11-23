using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockBehaviour : MonoBehaviour {

    public Sprite normalImg, tappedImg;

    bool tapped = false;
    bool isFirst = false;

    private int fila, columna;

    // Use this for initialization
    void Start () {
	}
	
    public void Tap()
    {
        if (!tapped)
        {
            if(GetComponentInParent<Grid>().CanBeTapped(this))
            {
                tapped = true;
                GetComponent<Image>().sprite = tappedImg;
                GetComponentInParent<Grid>().CheckFinish();
            }
        }
        else GetComponentInParent<Grid>().CheckPosibleUntap(this);
    }
    // devuelve true si se puede deseleccionar
    public bool Untap()
    {
        if (!isFirst && tapped)
        {
            tapped = false;
            GetComponent<Image>().sprite = normalImg;
            return true;
        }
        return false;
    }
    public bool IsTapped()
    {
        return tapped;
    }
    public void SetPos(int x, int y) {

        transform.position = new Vector3(x, y, 0);
    }
    public void SetCasilla(int f, int col)
    {
        fila = f;
        columna = col;
    }

    public int GetFila() { return fila; }
    public int GetColumna() { return columna; }

    public void SetFirst() {
        isFirst = tapped = true;
        GetComponent<Image>().sprite = tappedImg;
    }
}
