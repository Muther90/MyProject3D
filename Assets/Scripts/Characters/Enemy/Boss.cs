using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private RamAttack _ramAttack;

    public override void Initialize(ITargetable target)
    {
        base.Initialize(target);
        _ramAttack.Initialize(_target);
    }
}