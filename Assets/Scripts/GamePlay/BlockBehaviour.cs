﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour {

    public Sprite normalImg;
    public GameObject[] dirs;

    GameObject lastDir;

    public enum Dirs
    {
        left, right, up, down
    }
    Sprite tappedImg;

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
            CleanDirItems();
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
    public void ActiveDirItem(Dirs dir)
    {
        lastDir = dirs[(int)dir];
        lastDir.SetActive(true);
    }
    public void DisactiveLastDir()
    {
        if(lastDir)
            lastDir.SetActive(false);
    }
    public void CleanDirItems()
    {
        foreach (var x in dirs)
            x.SetActive(false);
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