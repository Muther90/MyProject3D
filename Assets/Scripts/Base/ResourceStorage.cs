using System;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public int CountResource { get; private set; }

    public event Action<int> ResourceValueChanged;

    public void AddResource()
    {
        CountResource++;
        ResourceValueChanged?.Invoke(CountResource);
    }

    public void TakeResource()
    {
        if (CountResource > 0)
        {
            CountResource--;
            ResourceValueChanged?.Invoke(CountResource);
        }
    }
}