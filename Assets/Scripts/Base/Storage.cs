using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public event Action<int> ResourceValueChanged;

    private int _countResource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IResource>(out IResource resource))
        {
            if (resource.IsCarried == false) 
            {
                resource.Reset();
                AddResource();
            }
        }
    }

    private void AddResource()
    {
        _countResource++;
        ResourceValueChanged?.Invoke(_countResource);
    }
}