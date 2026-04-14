using System;
using UnityEngine;

public class TargetProvider : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _targetBase;

    public event Action<ITargetable> TargetChanged;

    public ITargetable CurrentTarget { get; private set; }

    private void Awake()
    {
        CurrentTarget = _targetBase as ITargetable;
    }

    public void SetTarget(ITargetable target)
    {
        if (CurrentTarget != target)
        {
            CurrentTarget = target;
            TargetChanged?.Invoke(target);
        }
    }
}