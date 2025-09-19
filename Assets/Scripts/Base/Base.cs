using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private GlobalStorage _globalStorage;
    [SerializeField] private List<Worker> _workers;
    [SerializeField] private WorkerRetriever _workerRetriever;
    [SerializeField] private float _delay;

    private ResourceStorage _resourceStorage;
    private Vector3 _positionWorkerRetriever;
    private Coroutine _workCoroutine;
    private Queue<Worker> _freeWorkers = new();

    private void Awake()
    {
        _resourceStorage = GetComponent<ResourceStorage>();
        _positionWorkerRetriever = _workerRetriever.transform.position;
    }

    private void Start()
    {
        InitializeWorkers();
        _workCoroutine = StartCoroutine(WorkCoroutine());
    }

    private void OnEnable()
    {
        _workerRetriever.WorkerArrived += ServeWorker;
    }

    private void OnDisable()
    {
        _workerRetriever.WorkerArrived -= ServeWorker;
    }

    private IEnumerator WorkCoroutine()
    {
        WaitForSeconds wait = new(_delay);
        IResource foundResource;

        while (enabled)
        {
            foundResource = _globalStorage.GetAvailableResource();

            if (foundResource != null && _freeWorkers.Any())
            {
                Worker worker = _freeWorkers.Dequeue();
                worker.CarryThing(foundResource, _positionWorkerRetriever);
            }

            yield return wait;
        }

        _workCoroutine = null;
    }

    private void ServeWorker(Worker worker)
    {
        IResource deliveredResource = worker.GiveResource();

        if (deliveredResource != null)
        {
            _resourceStorage.AddResource();
            deliveredResource.Reset();
            _globalStorage.DeleteResource(deliveredResource);
            _freeWorkers.Enqueue(worker);
        }
    }

    private void InitializeWorkers()
    {
        foreach (Worker worker in _workers)
        {
            _freeWorkers.Enqueue(worker);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Add Child Workers")]
    private void RefreshChildArray()
    {
        _workers = new List<Worker>(GetComponentsInChildren<Worker>(true));
    }
#endif
}