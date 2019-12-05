using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    public uint levelIndex;
    public Text text;

    private void Start()
    {
        if(levelIndex<10)
            text.text = "00"+levelIndex.ToString();
        else if (levelIndex<100)
            text.text = "0" + levelIndex.ToString();
        else
            text.text = levelIndex.ToString();
    }
    public void StartLevel()
    {
        FindObjectOfType<GameManager>().StartLevel(levelIndex);
    }
}
