using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    private List<ResourceScanner> _resourceScanners = new();
    private readonly Dictionary<IResource, bool> _resources = new();

    private void OnEnable()
    {
        foreach (ResourceScanner resourceScanner in _resourceScanners)
        {
            resourceScanner.ResourceFounded += AddFoundResource;
        }
    }

    private void OnDisable()
    {
        foreach (ResourceScanner resourceScanner in _resourceScanners)
        {
            resourceScanner.ResourceFounded -= AddFoundResource;
        }
    }

    public void AddResourceScanner(ResourceScanner resourceScanner)
    {
        if (resourceScanner != null && _resourceScanners.Contains(resourceScanner) == false)
        {
            resourceScanner.ResourceFounded += AddFoundResource;
            _resourceScanners.Add(resourceScanner);
        }
    }

    public void RemoveResourceScanner(ResourceScanner resourceScanner)
    {
        if (resourceScanner != null && _resourceScanners.Contains(resourceScanner) == true)
        {
            resourceScanner.ResourceFounded -= AddFoundResource;
            _resourceScanners.Remove(resourceScanner);
        }
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