
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundTrigger3D : LayersTrigger3D
{
    public bool IsOnGround => IsExistAnyThinkAtTrigger;
}