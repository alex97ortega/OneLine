using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Pointer pointer;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        CheckPCInput();
#else
        CheckAndroidInput();
#endif
    }

#if UNITY_EDITOR
    void CheckPCInput()
    {
        if (Input.GetMouseButton(0))
        {
            pointer.TapPointer(Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pointer.UntapPointer();
        }
    }
#else
    void CheckAndroidInput()
    {
        if(Input.touchCount>0)
        {
            Touch tch = Input.GetTouch(0);
            pointer.TapPointer(tch.position.x, tch.position.y);

            if (tch.phase == TouchPhase.Ended)
                pointer.UntapPointer();
        }
    }
#endif
}
