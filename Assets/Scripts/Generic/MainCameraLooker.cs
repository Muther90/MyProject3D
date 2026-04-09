using System.Collections;
using UnityEngine;

public class MainCameraLooker : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.5f)] private float _updateInterval;
    [SerializeField] private Transform _currentTransform;

    private Transform _mainCamera;
    private Coroutine _watchCoroutine;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
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
            _currentTransform.transform.LookAt(_mainCamera);

            yield return wait;
        }

        _watchCoroutine = null;
    }
}