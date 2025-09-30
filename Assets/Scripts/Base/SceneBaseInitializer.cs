using UnityEngine;

public class SceneBaseInitializer : MonoBehaviour
{
    [SerializeField] private Base[] _allBasesOnScene;
    [SerializeField] private GlobalStorage _globalStorage;
    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private BaseCreator _baseCreator;
    [SerializeField] private FlagDeployer _flagDeployer;

    private void Awake()
    {
        foreach (Base baseInstance in _allBasesOnScene)
        {
            baseInstance.Initialize(_globalStorage, _workerCreator, _baseCreator, _flagDeployer);
            InitializeWorkersForBase(baseInstance);
        }
    }

    private void InitializeWorkersForBase(Base baseInstance)
    {
        Worker[] workers = baseInstance.GetComponentsInChildren<Worker>(true);

        foreach (Worker worker in workers)
        {
            baseInstance.AddWorker(worker);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Add Bases On Scene")]
    private void RefreshArray()
    {
        _allBasesOnScene = FindObjectsOfType<Base>(true);
    }
#endif
}