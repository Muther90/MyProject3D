using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    [SerializeField] private float _damageMultiplier;

    public IDamageable Owner { get; private set; }

    private void Awake()
    {
        if (transform.parent != null)
        {
            Owner = transform.parent.GetComponentInParent<IDamageable>();
        }
    }

    public void TakeDamage(float amount)
    {
        Owner.TakeDamage(amount * _damageMultiplier);
    }
}