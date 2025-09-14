using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _storageResources;
    [SerializeField] private List<Worker> _workers;

    private Coroutine _workCoroutine;
    private Vector3 _position;
    private Queue<Worker> _freeWorkers = new();
    private Queue<Vector3> _positionsFoundResources = new();

    private void Start()
    {
        _position = _storageResources.position;
        _workCoroutine = StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        WaitForSeconds wait = new(_delay);

        while (enabled)
        {
            ScanResources();
            AssignWorkers();

            yield return wait;
        }

        _workCoroutine = null;
    }

    private void ScanResources()
    {
        _positionsFoundResources.Clear();
        List<Collider> foundObjects = Scanner.Scan(_position, _radius);

        foreach (Collider collider in foundObjects)
        {
            if (collider.TryGetComponent<IResource>(out IResource resource))
            {
                if (resource.IsCarried == false)
                {
                    _positionsFoundResources.Enqueue(collider.transform.position);
                }
            }
        }
    }

    private void AssignWorkers()
    {
        UpdateFreeWorkers();

        while (_freeWorkers.Any() && _positionsFoundResources.Any())
        {
            Worker worker = _freeWorkers.Dequeue();
            worker.CarryThing(_positionsFoundResources.Dequeue(), _position);
        }
    }

    private void UpdateFreeWorkers()
    {
        _freeWorkers.Clear();

        foreach (Worker worker in _workers)
        {
            if (worker.IsBusy == false)
            {
                _freeWorkers.Enqueue(worker);
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Add Child Workers")]
    private void RefreshChildArray()
    {
        _workers?.Clear();
        _workers ??= new List<Worker>();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Worker>(out Worker worker))
            {
                _workers.Add(worker);
            }
        }
    }
#endif
}