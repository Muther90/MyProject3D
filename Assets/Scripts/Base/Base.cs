using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private WorkerRetriever _workerRetriever;
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private ResourceStorage _resourceStorage;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _workersSpawnPoint;

    private GlobalStorage _globalStorage;
    private WorkerCreator _workerCreator;
    private BaseCreator _baseCreator;
    private FlagDeployer _flagDeployer;
    private Vector3? _flagPosition;
    private bool _hasFlagDeployed;
    private Vector3 _positionWorkerRetriever;
    private Vector3 _spawnPosition;
    private Coroutine _workCoroutine;
    private Queue<Worker> _freeWorkers = new();

    public bool CanDeployFlag() => _freeWorkers.Count > 1;

    private void Start()
    {
        _positionWorkerRetriever = _workerRetriever.transform.position;
        _spawnPosition = _workersSpawnPoint.position;
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

    public void Initialize(GlobalStorage globalStorage, WorkerCreator workerCreator, BaseCreator baseCreator, FlagDeployer flagDeployer)
    {
        _globalStorage = globalStorage;
        _globalStorage.AddResourceScanner(_resourceScanner);
        _workerCreator = workerCreator;
        _baseCreator = baseCreator;
        _flagDeployer = flagDeployer;
    }

    public void AddWorker(Worker worker)
    {
        _freeWorkers.Enqueue(worker);
        worker.transform.SetParent(_workersSpawnPoint);
    }

    public void OnFlagDeployed(Vector3 worldPosition)
    {
        _flagPosition = worldPosition;
        _hasFlagDeployed = true;
    }

    private IEnumerator WorkCoroutine()
    {
        WaitForSeconds wait = new(_delay);

        while (enabled)
        {
            AssignWorkers();

            if (_hasFlagDeployed)
            {
                BuildBase();
            }
            else
            {
                HireWorker();
            }

            yield return wait;
        }

        _workCoroutine = null;
    }

    private void AssignWorkers()
    {
        if (_freeWorkers.Any())
        {
            IResource foundResource = _globalStorage.GetAvailableResource();

            if (foundResource != null) 
            {
                Worker worker = _freeWorkers.Dequeue();
                worker.CarryThing(foundResource, _positionWorkerRetriever);
            }
        }
    }

    private void HireWorker()
    {
        if (_resourceStorage.CountResource >= _workerCreator.Cost)
        {
            PayFromStorage(_workerCreator.Cost);

            _freeWorkers.Enqueue(_workerCreator.Create(_spawnPosition, _workersSpawnPoint));
        }
    }

    private void BuildBase()
    {
        if (_resourceStorage.CountResource >= _baseCreator.Cost && _freeWorkers.Any())
        {
            PayFromStorage(_baseCreator.Cost);

            Worker worker = _freeWorkers.Dequeue();
            worker.BuildBase(_flagPosition.Value, _baseCreator);

            _flagPosition = null;
            _hasFlagDeployed = false;
            _flagDeployer.RemoveFlagForBase(this);
        }
    }

    private void PayFromStorage(int pay)
    {
        for (int i = 0; i < pay; i++)
        {
            _resourceStorage.TakeResource();
        }
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
}