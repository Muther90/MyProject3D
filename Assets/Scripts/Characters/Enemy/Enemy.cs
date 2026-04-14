using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolObject, IInitializable<TargetProvider>, IResetable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Watcher _watcher;

    protected TargetProvider _targetProvider;
    protected bool _canMove;

    public event Action<IPoolObject> Returned;

    protected virtual void OnEnable()
    {
        _canMove = true;
        _health.Died += Died;
    }

    protected virtual void OnDisable()
    {
        _health.Died -= Died;
    }

    protected virtual void FixedUpdate()
    {
        if (_canMove && _targetProvider.CurrentTarget != null)
        {
            _mover.MoveTo(_targetProvider.CurrentTarget.Position - transform.position);
        }
    }

    public virtual void Reset()
    {
        _mover.Reset();
        _health.Reset();
        _targetProvider = null;
        _canMove = true;
    }

    public virtual void Initialize(TargetProvider targetProvider)
    {
        _targetProvider = targetProvider;
        _watcher.Initialize(_targetProvider);
    }

    public virtual void TakeDamage(float amount)
    {
        _health.ApplyDamage(amount);
    }

    public virtual void Died()
    {
        Returned?.Invoke(this);
    }

    protected void EnableMovement()
    {
        _canMove = true;
    }

    protected void DisableMovement()
    {
        _canMove = false;
    }
}