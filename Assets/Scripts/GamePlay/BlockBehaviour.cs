using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public Sprite normalImg;
    public GameObject mark;
    
    public enum Dirs
    {
        left, right, up, down
    }
    Sprite tappedImg;

    bool tapped = false;
    bool isFirst = false;

    private int fila, columna;

    Quaternion initialMarkRot;

    // Use this for initialization
    void Start ()
    {
        initialMarkRot = mark.transform.rotation;
	}
	
    public void Tap()
    {
        if (!tapped)
        {
            if(GetComponentInParent<Grid>().CanBeTapped(this))
            {
                tapped = true;
                GetComponent<SpriteRenderer>().sprite = tappedImg;
            }
        }
        else
            GetComponentInParent<Grid>().UntapOlders(this);
    }
    // devuelve true si se puede deseleccionar
    public bool Untap()
    {
        if (!isFirst && tapped)
        {
            DisactivateDirItem();
            tapped = false;
            GetComponent<SpriteRenderer>().sprite = normalImg;
            return true;
        }
        return false;
    }
    public bool IsTapped()
    {
        return tapped;
    }
    public void SetPos(float x, float y) {

        transform.position = new Vector3(x, y, 0);
    }
    public void SetCasilla(int f, int col)
    {
        fila = f;
        columna = col;
    }

    public int GetFila() { return fila; }
    public int GetColumna() { return columna; }
    public void SetSprite(Sprite newSprite) { tappedImg = newSprite; }

    public void SetFirst() {
        isFirst = tapped = true;
        GetComponent<SpriteRenderer>().sprite = tappedImg;
    }

    // esto es por pura estética, cuadraditos blancos que van 
    // mostrando visualmente el camino
    public void ActivateDirItem(Dirs dir)
    {
        mark.SetActive(true);
        switch(dir)
        {
            case Dirs.up:
                mark.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), 90);
                mark.transform.localPosition = new Vector3(0, 0.55f, 0);
                break;
            case Dirs.down:
                mark.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, -1), 90);
                mark.transform.localPosition = new Vector3(0, -0.55f, 0);
                break;
            case Dirs.left:
                mark.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 0, 1), 180);
                mark.transform.localPosition = new Vector3(-0.55f, 0, 0);
                break;
            // sin rotar
            case Dirs.right:
                mark.transform.localPosition = new Vector3(0.55f, 0, 0);
                break;
            default:
                break;
        }
    }
    public void DisactivateDirItem()
    {
        mark.transform.rotation = initialMarkRot;
        mark.SetActive(false);
    }
    public bool Inside(float x, float y)
    {
        float scaleX = transform.localScale.x/2;
        float scaleY = transform.localScale.y/2;

        if (x < (transform.position.x-scaleX) || x > (transform.position.x + scaleX))
            return false;
        if (y < (transform.position.y-scaleY) || y > (transform.position.y + scaleY))
            return false;
        return true;
    }
}
