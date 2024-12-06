using System;
using UnityEngine;

public class Bullet : BasePoolElement
{
    public event Action<Bullet, Collider> onTriggerEnter;

    public float DistanceToInfection => transform.localScale.x * _distanceCoefficientToInfection;

    [SerializeField] private float _speed;
    [SerializeField] private float _distanceCoefficientToInfection = 1.2f;


    public override void SetUse()
    {
        base.SetUse();
        gameObject.SetActive(true);
    }

    public override void SetUnUse()
    {
        base.SetUnUse();
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(IsUsing)
        {
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(this, other);
    }

    public void Destroy()
    {
        SetUnUse();
    }
}