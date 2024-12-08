using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrigger<TCollider> : MonoBehaviour
{
    public event Action<TCollider> onTriggerEnter;
    public event Action<TCollider> onTriggerExit;
    public event Action<TCollider> onTriggerStay;

    public bool IsExistAnyThinkAtTrigger => _collidersAtTrigger.Count > 0;
    protected readonly List<TCollider> _collidersAtTrigger = new List<TCollider>();

    protected abstract bool Check(TCollider collider);




    protected void OnEnter(TCollider collider)
    {
        if (Check(collider))
        {
            if (_collidersAtTrigger.Contains(collider) == false)
                _collidersAtTrigger.Add(collider);

            onTriggerEnter?.Invoke(collider);
        }
    }

    protected void OnExit(TCollider collider)
    {
        if (Check(collider))
        {
            if (_collidersAtTrigger.Contains(collider))
                _collidersAtTrigger.Remove(collider);

            onTriggerExit?.Invoke(collider);
        }
    }

    protected void OnStay(TCollider collider)
    {
        if (Check(collider))
        {
            if (_collidersAtTrigger.Contains(collider) == false)
                _collidersAtTrigger.Add(collider);

            onTriggerStay?.Invoke(collider);
        }
    }
}