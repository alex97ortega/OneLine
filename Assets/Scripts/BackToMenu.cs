using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void Back()
    {
        FindObjectOfType<GameManager>().ChangeScene("MainMenu");
    }
}
