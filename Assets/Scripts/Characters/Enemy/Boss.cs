using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private RamAttack _ramAttack;

    public override void Initialize(TargetProvider targetProvider)
    {
        base.Initialize(targetProvider);
        _ramAttack.Initialize(targetProvider);
    }
}