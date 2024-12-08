using System;
using UnityEngine;

public class Bullet : BasePoolElement
{
    public event Action<Bullet, Collider> onTriggerEnter;

    public float DistanceToInfection => transform.localScale.x * _distanceCoefficientToInfection;

    [SerializeField] private float _speed;
    [SerializeField] private float _duration = 3;
    [SerializeField] private float _distanceCoefficientToInfection = 1.2f;
    private float _currentTime = 0f;

    public override void SetUse()
    {
        base.SetUse();
        _currentTime = 0f;
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
            _currentTime += Time.fixedDeltaTime;
            if(_currentTime >= _duration)
            {
                SetUnUse();
                return;
            }

            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IInfectible infectible))
        {
            if(infectible.IsInfected == false)
            {
                float power = transform.localScale.x;
                infectible.Infect(power);
            }
        }
        onTriggerEnter?.Invoke(this, collider);
        Destroy();
    }

    public void Destroy()
    {
        SetUnUse();
    }
}