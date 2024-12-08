using System;
using UnityEngine;

public class MobileInputHandler : IInputHandler
{
    public event Action onScreenPointerDown;
    public event Action onScreenPointerUp;


    public void Update()
    {
        if(Input.touches.Length == 1)
        {
            var touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    onScreenPointerDown?.Invoke();
                    break;

                case TouchPhase.Ended:
                    onScreenPointerUp?.Invoke();
                    break;
            }
        }
    }
}