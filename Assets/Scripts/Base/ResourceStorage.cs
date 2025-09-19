using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private int _countResource;

    public event Action<int> ResourceValueChanged;

    public void AddResource()
    {
        _countResource++;
        ResourceValueChanged?.Invoke(_countResource);
    }
}