using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event Action<Obstacle> onInfected;
    public event Action<Obstacle> onDestroyed;

    public bool IsDestroyed { get; private set; } = false;
    public bool IsInfected { get; private set; } = false;

    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _infectedMaterial;


    public void Infect()
    {
        IsInfected = true;
        _meshRenderer.material = _infectedMaterial;
        this.DoAction(_timeToDestroy, Destroy);
        onInfected?.Invoke(this);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        IsDestroyed = true;
        onDestroyed?.Invoke(this);
    }
}