using UnityEngine;

public class BaseCreator : Creator<Base> 
{
    [SerializeField] private GlobalStorage _globalStorage;
    [SerializeField] private WorkerCreator _workerCreator;
    [SerializeField] private FlagDeployer _flagDeployer;

    protected override void InitializeInstance(Base baseInstance)
    {
        baseInstance.Initialize(_globalStorage, _workerCreator, this, _flagDeployer);
    }
}