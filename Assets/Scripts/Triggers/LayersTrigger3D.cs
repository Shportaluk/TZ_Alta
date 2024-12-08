using System.Collections.Generic;
using UnityEngine;

public class LayersTrigger3D : AbstractTrigger<Collider>
{
    [SerializeField] private List<int> _layers;
    [SerializeField] private bool _checkLayers = true;


    protected override bool Check(Collider collider)
    {
        if (_checkLayers)
        {
            return _layers.Contains(collider.gameObject.layer);
        }

        return true;
    }



    private void OnTriggerEnter(Collider collider)
    {
        OnEnter(collider);
    }


    private void OnTriggerExit(Collider collider)
    {
        OnExit(collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        OnStay(collider);
    }
}