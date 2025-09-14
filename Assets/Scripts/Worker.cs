using System.Collections;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Collector _collector;

    private Coroutine _taskCoroutine;

    public bool IsBusy { get; private set; }

    public void CarryThing(Vector3 from, Vector3 target)
    {
        if (_taskCoroutine != null)
        {
            StopCoroutine(_taskCoroutine);
            IsBusy = false;
        }

        _taskCoroutine = StartCoroutine(CarryThingCoroutine(from, target));
    }

    private IEnumerator CarryThingCoroutine(Vector3 from, Vector3 target)
    {
        IsBusy = true;

        yield return StartCoroutine(_mover.MoveTo(from));
        _collector.TakeThing();

        if (_collector.IsCarrying == true) 
        {
            yield return StartCoroutine(_mover.MoveTo(target));
            _collector.DropThing();
        }

        IsBusy = false;
        _taskCoroutine = null;
    }
}
