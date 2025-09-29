using UnityEngine;

public class BaseCreator : Creator<Base> 
{
    [SerializeField] private GlobalStorage _globalStorage;
    [SerializeField] private WorkerCreator _workerCreator;

    protected override void InitializeInstance(Base baseInstance)
    {
        baseInstance.Initialize(_globalStorage, _workerCreator, this);
    }
}