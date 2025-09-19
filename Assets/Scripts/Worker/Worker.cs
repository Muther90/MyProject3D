using System.Collections;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Transform _holdPoint;

    private Coroutine _taskCoroutine;
    private IResource _backpuck;

    public void CarryThing(IResource resource, Vector3 target)
    {
        if (_taskCoroutine != null)
        {
            StopCoroutine(_taskCoroutine);
            _taskCoroutine = null;
        }

        _taskCoroutine = StartCoroutine(CarryResourceCoroutine(resource, target));
    }

    public IResource GiveResource()
    {
        if (_backpuck != null)
        {
            IResource resourceToGive = _backpuck;
            _backpuck.Drop();
            _backpuck = null;

            return resourceToGive;
        }

        return null;
    }

    private IEnumerator CarryResourceCoroutine(IResource resource, Vector3 target)
    {
        yield return _mover.MoveTo(resource.Position);

        resource.PickUp(transform, _holdPoint);
        _backpuck = resource;

        yield return _mover.MoveTo(target);

        _taskCoroutine = null;
    }
}