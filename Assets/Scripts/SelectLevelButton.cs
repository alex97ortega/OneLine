using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    public Text text;
    public Image imgLocked;
    uint levelIndex;
    ScenesManager scenesManager;

    bool unlocked;

    private void Start()
    {
        scenesManager = FindObjectOfType<ScenesManager>();        
    }
    public void StartLevel()
    {
        if(unlocked)
            scenesManager.ChangeToGamePlay(levelIndex);
    }
    public void Unlock()
    {
        imgLocked.gameObject.SetActive(false);
        unlocked = true;
    }
    void Lock()
    {
        imgLocked.gameObject.SetActive(true);
        unlocked = false;
    }
    // esto se llama nada más ser creados los botones
    public void SetLevel(uint lvl)
    {
        levelIndex = lvl;

        if (levelIndex < 10)
            text.text = "00" + levelIndex.ToString();
        else if (levelIndex < 100)
            text.text = "0" + levelIndex.ToString();
        else
            text.text = levelIndex.ToString();

        // al iniciar siempre están todos los botones bloqueados

        Lock();
    }
}
