using System.Collections;
using UnityEngine;

public class DistanceDetector : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Min(0)] private float _detectionRange = 5f;
    [SerializeField, Min(0)] private float _updateInterval = 0.4f;

    private float _sqrDetectionRange;
    private Coroutine _detectionCoroutine;

    public bool TargetDetected { get; private set; }
    public Vector3 Target => _target.position;

    private void OnEnable()
    {
        _detectionCoroutine = StartCoroutine(DetectionCoroutine());
    }

    private void OnDisable()
    {
        if (_detectionCoroutine != null)
        {
            StopCoroutine(_detectionCoroutine);
            _detectionCoroutine = null;
        }
    }

    private IEnumerator DetectionCoroutine()
    {
        WaitForSeconds wait = new(_updateInterval);
        _sqrDetectionRange = _detectionRange * _detectionRange;
        float sqrDistance;

        while (enabled)
        {
            sqrDistance = (_target.position - transform.position).sqrMagnitude;

            if (sqrDistance <= _sqrDetectionRange)
            {
                TargetDetected = true;
            }
            else
            {
                TargetDetected = false;
            }

            yield return wait;
        }
    }
}