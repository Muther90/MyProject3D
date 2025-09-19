using System;
using UnityEngine;

public class WorkerRetriever : MonoBehaviour
{
    private Collider _collider;

    public event Action<Worker> WorkerArrived;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Worker>(out Worker worker))
        {
            WorkerArrived(worker);
        }
    }
}