using System;
using UnityEngine;

public class Obstacle : MonoBehaviour, IInfectible
{
    public event Action<Obstacle> onInfected;
    public event Action<Obstacle> onDestroyed;

    public bool IsDestroyed { get; private set; } = false;
    public bool IsInfected { get; private set; } = false;

    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private float _radiusInfectMultiplier = 10;
    [SerializeField] private float _delayToInfectNext = 0.25f;
    [SerializeField] private float _powerToNextInfectMultiplier = 0.5f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _infectedMaterial;


    public void Infect(float power)
    {
        IsInfected = true;
        _meshRenderer.material = _infectedMaterial;
        this.DoAction(_timeToDestroy, Destroy);
        onInfected?.Invoke(this);

        this.DoAction(_delayToInfectNext, () => InfectOther(power));
    }

    private void InfectOther(float power)
    {
        float radius = power * _radiusInfectMultiplier;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IInfectible infectible))
            {
                if(infectible.IsInfected == false)
                {
                    infectible.Infect(power * _powerToNextInfectMultiplier);
                }
            }
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        IsDestroyed = true;
        onDestroyed?.Invoke(this);
    }
}