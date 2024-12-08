using UnityEngine;

public static class PhysicUtility
{
    public static bool Check(Vector3 pointA, Vector3 pointB, float boxWidth, int layerMask, out RaycastHit[] hits)
    {
        Vector3 direction = pointB - pointA;
        float distance = direction.magnitude;
        direction.Normalize();

        Vector3 boxHalfExtents = new Vector3(boxWidth / 2, boxWidth / 2, 0.1f);

        hits = Physics.BoxCastAll(
            pointA,                       
            boxHalfExtents,
            direction,
            Quaternion.identity,
            distance,
            layerMask
        );

        return hits.Length > 0;
    }
}