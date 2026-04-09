using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IPoolObject, IInitializable<ITargetable>, IResetable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Watcher _watcher;
    [SerializeField] private Health _health;

    protected ITargetable _target;
    protected bool _canMove;

    public event Action<IPoolObject> Returned;

    protected virtual void OnEnable()
    {
        _canMove = true;

        _health.Died += Died;

        if (_target != null)
        {
            _watcher.SetTarget(_target);
            _watcher.StartWatching();
        }
    }

    protected virtual void OnDisable()
    {
        _health.Died -= Died;

        if (_target != null)
        {
            _watcher.StopWatching();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_canMove && _target != null)
        {
            _mover.MoveTo(_target.Position - transform.position);
        }
    }

    public virtual void Reset()
    {
        _mover.Reset();
        _health.Reset();
        _target = null;
        _canMove = true;
    }

    public virtual void Initialize(ITargetable target)
    {
        _target = target;
        _watcher.SetTarget(_target);
        _watcher.StartWatching();
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