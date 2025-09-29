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
        Stop();
        _taskCoroutine = StartCoroutine(CarryResourceCoroutine(resource, target));
    }

    public void BuildBase(Vector3 target, BaseCreator baseCreator)
    {
        Stop();
        _taskCoroutine = StartCoroutine(BuildBaseCoroutine(target, baseCreator));
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

    private IEnumerator BuildBaseCoroutine(Vector3 target, BaseCreator baseCreator)
    {
        yield return _mover.MoveTo(target);

        Base newBase = baseCreator.Create(target);
        newBase.AddWorker(this);

        _taskCoroutine = null;
    }

    private void Stop()
    {
        if (_taskCoroutine != null)
        {
            StopCoroutine(_taskCoroutine);
            _taskCoroutine = null;
        }
    }
}