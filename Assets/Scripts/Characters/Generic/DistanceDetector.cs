using System;
using System.Collections;
using UnityEngine;

public class DistanceDetector : MonoBehaviour, IInitializable<ILocatable>
{
    [SerializeField, Min(0)] private float _detectionRange = 5f;
    [SerializeField, Min(0)] private float _updateInterval = 0.4f;

    private float _sqrDetectionRange;
    private Coroutine _detectionCoroutine;
    private ILocatable _target;

    public event Action TargetDetected;

    private void OnEnable()
    {
        if (_target != null) 
        {
            _detectionCoroutine = StartCoroutine(DetectionRoutine());
        }
    }

    private void OnDisable()
    {
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);
        }
    }

    public void Initialize(ILocatable target)
    {
        _target = target;
        _detectionCoroutine = StartCoroutine(DetectionRoutine());
    }

    private IEnumerator DetectionRoutine()
    {
        WaitForSeconds wait = new(_updateInterval);
        _sqrDetectionRange = _detectionRange * _detectionRange;
        float sqrDistance;

        while (enabled)
        {
            sqrDistance = (_target.Position - transform.position).sqrMagnitude;

            if (sqrDistance <= _sqrDetectionRange)
            {
                TargetDetected?.Invoke();
            }

            yield return wait;
        }
    }
}