using System.Collections;
using UnityEngine;

public class Watcher : MonoBehaviour, IInitializable<TargetProvider>
{
    private const float MinSqrMagnitude = 0.0001f;

    [SerializeField, Min(0f)] private float _rotationSpeedDegree;
    [SerializeField, Range(0.01f, 0.5f)] private float _updateInterval = 0.05f;

    private TargetProvider _targetProvider;
    private Coroutine _watchCoroutine;

    private void OnDisable()
    {
        if (_watchCoroutine != null)
        {
            StopCoroutine(_watchCoroutine);
            _watchCoroutine = null;
        }
    }

    public void Initialize(TargetProvider targetProvider)
    {
        _targetProvider = targetProvider;
        _watchCoroutine = StartCoroutine(WatchCoroutine());
    }

    private IEnumerator WatchCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_updateInterval);
        float step;
        Quaternion targetRot;
        Vector3 direction;

        while (_targetProvider.CurrentTarget != null)
        {
            direction = _targetProvider.CurrentTarget.Position - transform.position;
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