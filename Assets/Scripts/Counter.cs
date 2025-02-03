using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour
{
    public event System.Action<float> CountChanged;

    [SerializeField] private float _startCount;
    [SerializeField] private float _stepIncreaseCount;
    [SerializeField] private float _updateInterval;

    private bool _isCounterRunning = false;
    private float _currentCount;
    private Coroutine _counterCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _currentCount = _startCount;
        CountChanged.Invoke(_currentCount);
        _waitForSeconds = new WaitForSeconds(_updateInterval);
    }

    private void Update()
    {
        MouseInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isCounterRunning)
            {
                StopCounter();
            }
            else
            {
                StartCounter();
            }
        }
    }

    private void StartCounter()
    {
        _isCounterRunning = true;
        _counterCoroutine = StartCoroutine(CounterCoroutine());
    }

    private void StopCounter()
    {
        _isCounterRunning = false;
        StopCoroutine(_counterCoroutine);
    }

    private IEnumerator CounterCoroutine()
    {
        while (_isCounterRunning)
        {
            CountChanged.Invoke(_currentCount += _stepIncreaseCount);

            yield return _waitForSeconds;
        }
    }
}
