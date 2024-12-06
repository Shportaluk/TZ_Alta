using System;
using UnityEngine;

public class PCInputHandler : IInputHandler
{
    public event Action onScreenPointerDown;
    public event Action onScreenPointerUp;


    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onScreenPointerDown?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onScreenPointerUp?.Invoke();
        }
    }
}