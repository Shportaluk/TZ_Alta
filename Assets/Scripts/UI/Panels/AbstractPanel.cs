using System;
using UnityEngine;

public abstract class AbstractPanel : MonoBehaviour, IPanel
{
    public abstract void Show();
    public abstract void Hide();
}