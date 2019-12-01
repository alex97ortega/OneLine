using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class ChangeSpriteButton : MonoBehaviour {

    public Sprite normal, pressed;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeOnclick);
    }
    private void ChangeOnclick()
    {
        GetComponent<Image>().sprite = pressed;
    }
}
