using System;

public interface IInputHandler : IHandler
{
    event Action onScreenPointerDown;
    event Action onScreenPointerUp;
}