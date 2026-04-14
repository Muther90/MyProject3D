using System;
using System.Collections;
using UnityEngine;

public class DistanceDetector : MonoBehaviour, IInitializable<TargetProvider>
{
    [SerializeField, Min(0)] private float _detectionRange = 5f;
    [SerializeField, Min(0)] private float _updateInterval = 0.4f;

    private float _sqrDetectionRange;
    private Coroutine _detectionCoroutine;
    private TargetProvider _targetProvider;

    public event Action TargetDetected;

    private void OnDisable()
    {
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);
            _detectionCoroutine = null;
        }
    }

    public void Initialize(TargetProvider targetProvider)
    {
        _targetProvider = targetProvider;
        _detectionCoroutine = StartCoroutine(DetectionCoroutine());
    }

    private IEnumerator DetectionCoroutine()
    {
        WaitForSeconds wait = new(_updateInterval);
        _sqrDetectionRange = _detectionRange * _detectionRange;
        float sqrDistance;

        while (_targetProvider.CurrentTarget != null)
        {
            sqrDistance = (_targetProvider.CurrentTarget.Position - transform.position).sqrMagnitude;

            if (sqrDistance <= _sqrDetectionRange)
            {
                TargetDetected?.Invoke();
            }

            yield return wait;
        }
    }
}