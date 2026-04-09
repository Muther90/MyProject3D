using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] private float _damageMultiplier;

    private IDamageable _owner;

    public IDamageable Owner => _owner;

    private void Awake()
    {
        if (transform.parent != null)
        {
            _owner = transform.parent.GetComponentInParent<IDamageable>();
        }
    }

    public void TakeDamage(float amount)
    {
        _owner?.TakeDamage(amount * _damageMultiplier);
    }
}