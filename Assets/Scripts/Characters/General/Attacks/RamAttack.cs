using System.Collections;
using UnityEngine;

public class RamAttack : MonoBehaviour, IInitializable<TargetProvider>
{
    [SerializeField, Min(0)] private float _damage = 50f;
    [SerializeField, Min(0)] private float _hitCooldown = 0.5f;

    private Coroutine _cooldownCoroutine;
    private TargetProvider _targetProvider;
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

    public void Initialize(TargetProvider targetProvider)
    {
        _targetProvider = targetProvider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canAttack)
        {
            IDamageable currentTarget = _targetProvider.CurrentTarget;

            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                IDamageable validTarget = TargetValidator.GetValidTarget(damageable, _targetProvider.CurrentTarget);

                if (validTarget != null)
                {
                    validTarget.TakeDamage(_damage);
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