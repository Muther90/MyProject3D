using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private MeleeWeaponData _data;
    [SerializeField] private Collider _attackCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private MeleeAnimationEvents _meleeAnimationEvents;

    private Transform _transform;
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;

    public override WeaponData Data => _data;

    private readonly HashSet<IDamageable> _hitTargets = new();

    private void Awake()
    {
        _transform = transform;
        _initialLocalPosition = _transform.localPosition;
        _initialLocalRotation = _transform.localRotation;

        _attackCollider.isTrigger = true;
        _attackCollider.enabled = false;

        _animator.runtimeAnimatorController = _data.OverrideController;
        _animator.writeDefaultValuesOnDisable = true;
    }

    private void OnEnable()
    {
        _transform.SetLocalPositionAndRotation(_initialLocalPosition, _initialLocalRotation);
        _meleeAnimationEvents.OnAttackStart += EnableCollider;
        _meleeAnimationEvents.OnAttackEnd += DisableCollider;
        DisableCollider();
    }

    private void OnDisable()
    {
        _meleeAnimationEvents.OnAttackStart -= EnableCollider;
        _meleeAnimationEvents.OnAttackEnd -= DisableCollider;
        DisableCollider();
    }

    public override void Reset()
    {
        _transform.SetLocalPositionAndRotation(_initialLocalPosition, _initialLocalRotation);
        DisableCollider();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            IDamageable hashKey = damageable;

            if (damageable is DamageReceiver damageReceiver && damageReceiver.Owner != null)
            {
                hashKey = damageReceiver.Owner;
            }

            if (_hitTargets.Contains(hashKey) == false)
            {
                _hitTargets.Add(hashKey);
                damageable.TakeDamage(_data.Damage);
            }
        }
    }

    public override void Attack()
    {
        _hitTargets.Clear();
        _animator.SetTrigger(MeleeAnimatorData.Attack);
    }

    private void EnableCollider() => _attackCollider.enabled = true;
    private void DisableCollider() => _attackCollider.enabled = false;
}