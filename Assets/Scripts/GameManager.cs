using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static IInputHandler InputHandler { get; private set; }

    private static readonly List<IHandler> _handlers = new List<IHandler>();


    private void Awake()
    {
        InitHandlers();
    }

    private static void InitHandlers()
    {
        InputHandler = new PCInputHandler();
        _handlers.Add(InputHandler);
    }

    private void Update()
    {
        foreach (var handler in _handlers)
        {
            handler.Update();
        }
    }
}