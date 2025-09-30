using System;
using UnityEngine;

public class Flag : MonoBehaviour, IPoolObject
{
    public event Action<IPoolObject> Taken;

    public void Reset() 
    {
        Taken?.Invoke(this);
    }

    public void PlaceAt(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }
}