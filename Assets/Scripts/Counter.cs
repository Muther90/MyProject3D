using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _startCount;
    [SerializeField] private float _stepIncreaseCounter;
    [SerializeField] private float _updateInterval;
    [SerializeField] private CounterView _counterView;

    private bool _isCounterRunning = false;
    private float _currentCount;
    private Coroutine _counterCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _currentCount = _startCount;
        _counterView.UpdateCountText(_currentCount);
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
            _currentCount += _stepIncreaseCounter;
            _counterView.UpdateCountText(_currentCount);

            yield return _waitForSeconds;
        }
    }
}
