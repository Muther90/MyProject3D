using UnityEngine;
using System.Collections;

public class RamAttack : MonoBehaviour, IInitializable<ITargetable>
{
    [SerializeField, Min(0)] private float _damage = 50f;
    [SerializeField, Min(0)] private float _hitCooldown = 0.5f;

    private Coroutine _cooldownCoroutine;
    private ITargetable _target;
    private bool _canAttack;

    private void OnEnable()
    {
        _canAttack = true;  
    }

    private void OnDisable()
    {
        if (_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
            _cooldownCoroutine = null;
        }
    }

    public void Initialize(ITargetable target)
    {
        _target = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canAttack)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if (damageable == _target)
                {
                    _target.TakeDamage(_damage);
                    StartHitCooldown();
                }
                else if (damageable is DamageReceiver damageReceiver && damageReceiver.Owner == _target)
                {
                    damageable.TakeDamage(_damage);
                    StartHitCooldown();
                }
            }
        }
    }

    private void StartHitCooldown()
    {
        _canAttack = false;
        _cooldownCoroutine = StartCoroutine(HitCooldownRoutine());
    }

    private IEnumerator HitCooldownRoutine()
    {
        yield return new WaitForSeconds(_hitCooldown);
        _canAttack = true;
    }
}