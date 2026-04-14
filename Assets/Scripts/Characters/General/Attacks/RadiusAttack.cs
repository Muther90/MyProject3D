using UnityEngine;

public class RadiusAttack : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damage = 10f;
    [SerializeField, Min(0)] private float _radius = 10f;
    [SerializeField, Min(0)] private float _heightUp = 1f;
    [SerializeField, Min(0)] private float _heightDown = 1f;

    private readonly Collider[] _hits = new Collider[14];

    public void TryDealDamage(IDamageable target)
    {
        Vector3 top = transform.position + Vector3.up * _heightUp;
        Vector3 bottom = transform.position - Vector3.up * _heightDown;

        int hitCount = Physics.OverlapCapsuleNonAlloc(top, bottom, _radius, _hits);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = _hits[i];

            if (hit.TryGetComponent(out IDamageable damageable))
            {
                IDamageable validTarget = TargetValidator.GetValidTarget(damageable, target);

                if (validTarget != null)
                {
                    validTarget.TakeDamage(_damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 top = transform.position + Vector3.up * _heightUp;
        Vector3 bottom = transform.position - Vector3.up * _heightDown;

        Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
        Gizmos.DrawLine(top, bottom);

        DrawDisc(top);
        DrawDisc(bottom);
    }

    private void DrawDisc(Vector3 center)
    {
        for (int i = 0; i < 16; i++)
        {
            float a = i * 22.5f * Mathf.Deg2Rad;
            float b = (i + 1) * 22.5f * Mathf.Deg2Rad;

            Vector3 p1 = center + new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a)) * _radius;
            Vector3 p2 = center + new Vector3(Mathf.Cos(b), 0, Mathf.Sin(b)) * _radius;

            Gizmos.DrawLine(p1, p2);
        }
    }
}