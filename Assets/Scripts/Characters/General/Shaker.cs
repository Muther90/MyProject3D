using System;
using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour, IResetable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField, Min(0.6f)] private float _duration;
    [SerializeField, Min(0f)] private float _force;
    [SerializeField, Min(0f)] private float _returnForce;
    [SerializeField, Range(0.01f, 0.5f)] private float _updateInterval;

    private Coroutine _shakeCoroutine;
    private Vector3 _baseShakePosition;

    public void Reset()
    {
        ResetState();
    }

    public void StartShake(Action ShakeCompleted)
    {
        ResetState();

        _shakeCoroutine = StartCoroutine(ShakeCoroutine(ShakeCompleted));
    }

    private IEnumerator ShakeCoroutine(Action ShakeCompleted)
    {
        _rigidbody.velocity = Vector3.zero;
        WaitForSeconds wait = new (_updateInterval);
        float endTime = Time.time + _duration;
        _baseShakePosition = transform.position;

        while (Time.time < endTime)
        {
            ApplyPushForce();
            ApplyReturnForce();

            yield return wait;
        }

        _shakeCoroutine = null;
        ShakeCompleted?.Invoke();
    }

    private void ApplyPushForce()
    {
        Vector2 direction2D = UnityEngine.Random.insideUnitCircle;
        Vector3 direction3D = new(direction2D.x, 0f, direction2D.y);
        _rigidbody.AddForce(direction3D * _force, ForceMode.Force);
    }

    private void ApplyReturnForce()
    {
        Vector3 offset = _baseShakePosition - transform.position;
        offset.y = 0f;
        _rigidbody.AddForce(offset * _returnForce, ForceMode.VelocityChange);
    }

    private void ResetState()
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = null;
        }

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}