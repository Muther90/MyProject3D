using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private Clickability _clickability;

    private void OnEnable()
    {
        _clickability.OnObjectClicked += Explode;
    }

    private void OnDisable()
    {
        _clickability.OnObjectClicked -= Explode;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionForce);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> explodableObjects = new();

        foreach (Collider hit in hits) 
        { 
            if (hit.attachedRigidbody != null)
            {
                explodableObjects.Add(hit.attachedRigidbody);
            }
        }

        return explodableObjects;
    }
}
