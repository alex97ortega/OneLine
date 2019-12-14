using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Camera mainCamera;
    public Pointer pointer;
    public Grid grid;

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
            OnTouchDown(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnTouchUp();
        }
    }
#else
    void CheckAndroidInput()
    {
        if(Input.touchCount>0)
        {
            Touch tch = Input.GetTouch(0);
            switch (tch.phase)
            {
                case TouchPhase.Began:
                    OnTouchDown(tch.position);
                    break;
                case TouchPhase.Ended:
                    OnTouchUp();
                    break;
                case TouchPhase.Moved:
                    OnTouchDown(tch.position);
                    break;
            }                
        }
    }
#endif
    private Vector2 ToWorldPoint(Vector3 screenPoint)
    {
        Vector3 vect = new Vector3(screenPoint.x, screenPoint.y, -10);
        Vector2 ret = mainCamera.ScreenToWorldPoint(vect);
        return ret;
    }
    private void OnTouchDown(Vector3 screenPoint)
    {
        Vector2 worldPos = ToWorldPoint(screenPoint);
        pointer.TapPointer(-worldPos.x, -worldPos.y);
        grid.OnTouchDown(-worldPos.x, -worldPos.y);
    }
    private void OnTouchUp()
    {
        pointer.UntapPointer();
        grid.CheckFinish();
    }
}
