using System.Collections;
using UnityEngine;

public class CanvasFollower : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.5f)] private float _updateInterval;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _follower;

    private float _heightOffset;
    private Coroutine _watchCoroutine;
    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        _heightOffset = transform.position.y - _target.position.y;
    }

    private void OnEnable()
    {
        if (_watchCoroutine == null)
        {
            _watchCoroutine = StartCoroutine(WatchCoroutine());
        }
    }

    private void OnDisable()
    {
        if (_watchCoroutine != null)
        {
            StopCoroutine(_watchCoroutine);
            _watchCoroutine = null;
        }
    }

    private IEnumerator WatchCoroutine()
    {
        WaitForSeconds wait = new(_updateInterval);

        while (enabled)
        {
            Vector3 worldPos = _target.position;
            worldPos.y += _heightOffset;

            _follower.position = worldPos;
            _follower.LookAt(_mainCamera);

            yield return wait;
        }

        _watchCoroutine = null;
    }
}