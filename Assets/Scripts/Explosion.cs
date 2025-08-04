using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _baseExplosionRadius;
    [SerializeField] private float _baseExplosionForce;
    [SerializeField] private float _forceFalloff;

    private Collider[] _hitColliders = new Collider[20];
    private int numColliders;

    public void Explode(Vector3 position)
    {
        numColliders = Physics.OverlapSphereNonAlloc(position, _baseExplosionRadius, _hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (_hitColliders[i].TryGetComponent(out Rigidbody rigidbody))
            {
                ApplyExplosionForce(rigidbody, position);
            }
        }
    }

    private void ApplyExplosionForce(Rigidbody rigidbody, Vector3 explosionPosition)
    {
        Vector3 direction = rigidbody.position - explosionPosition;

        float sqrDistance = direction.sqrMagnitude;
        float sqrRadius = _baseExplosionRadius * _baseExplosionRadius;

        if (sqrDistance < sqrRadius) 
        {
            direction.Normalize();
            float forceMagnitude = Mathf.Pow(_baseExplosionForce * (_baseExplosionRadius - Mathf.Sqrt(sqrDistance)) / _baseExplosionRadius, _forceFalloff);
            rigidbody.AddForce(direction * forceMagnitude, ForceMode.Impulse);
        }
    }
}