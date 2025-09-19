using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    [SerializeField] private ResourceScanner _resourceScanner;

    private readonly Dictionary<IResource, bool> _resources = new();

    private void OnEnable()
    {
        _resourceScanner.ResourceFounded += AddFoundResource;
    }

    private void OnDisable()
    {
        _resourceScanner.ResourceFounded -= AddFoundResource;
    }

    public IResource GetAvailableResource()
    {
        foreach (var pair in _resources)
        {
            if (pair.Value == true)
            {
                IResource resource = pair.Key;
                _resources[resource] = false;

                return resource;
            }
        }

        return null;
    }

    public void DeleteResource(IResource resource)
    {
        _resources.Remove(resource);
    }

    private void AddFoundResource(IResource resource)
    {
        _resources.TryAdd(resource, true);
    }
}