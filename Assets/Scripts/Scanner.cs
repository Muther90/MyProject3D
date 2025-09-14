using System.Collections.Generic;
using UnityEngine;

public static class Scanner
{
    private static Collider[] _buffer = new Collider[100];

    public static List<Collider> Scan(Vector3 center, float radius)
    {
        List<Collider> foundObjects = new();
        int count = Physics.OverlapSphereNonAlloc(center, radius, _buffer);

        for (int i = 0; i < count; i++)
        {
            foundObjects.Add(_buffer[i]);
        }

        return foundObjects;
    }
}