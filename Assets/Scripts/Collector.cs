using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private float _radiusPickUp;
    [SerializeField] private Transform _holdPoint;

    private ICarriable _carriedThing;

    public bool IsCarrying => _carriedThing != null;

    public void TakeThing()
    {
        List<Collider> foundObjects = Scanner.Scan(transform.position, _radiusPickUp);

        foreach (Collider collider in foundObjects)
        {
            if (collider.TryGetComponent<ICarriable>(out ICarriable carriable))
            {
                if (carriable.IsCarried == false)
                {
                    DropThing();
                    carriable.PickUp(transform, _holdPoint);
                    _carriedThing = carriable;

                    break;
                }
            }
        }
    }

    public void DropThing()
    {
        if (_carriedThing != null) 
        { 
            _carriedThing.Drop();
            _carriedThing = null;
        }
    }
}