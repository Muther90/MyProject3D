using System.Collections;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    private const float MinSqrMagnitude = 0.0001f;

    [SerializeField, Min(0f)] private float _rotationSpeedDegree;
    [SerializeField, Range(0.01f, 0.5f)] private float _updateInterval = 0.05f;

    private ILocatable _target;
    private Coroutine _watchCoroutine;

    private void OnEnable()
    {
        if (_target != null && _watchCoroutine == null)
        {
            _watchCoroutine = StartCoroutine(WatchCoroutine());
        }
    }

    private void OnDisable()
    {
        StopWatching();
    }

    public void SetTarget(ILocatable target)
    {
        _target = target;
    }

    public void StartWatching()
    {
        if (_target != null)
        {
            StopWatching();
            _watchCoroutine = StartCoroutine(WatchCoroutine());
        }
    }

    public void StopWatching()
    {
        if (_watchCoroutine != null)
        {
            StopCoroutine(_watchCoroutine);
            _watchCoroutine = null;
        }
    }

    private IEnumerator WatchCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_updateInterval);
        float step;
        Quaternion targetRot;
        Vector3 direction;

        while (_target != null)
        {
            direction = _target.Position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > MinSqrMagnitude)
            {
                targetRot = Quaternion.LookRotation(direction);
                step = _rotationSpeedDegree * _updateInterval;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step);
            }

            yield return wait;
        }

        _watchCoroutine = null;
    }
}