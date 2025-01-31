using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Count : MonoBehaviour
{
    [SerializeField] private float _startCount;
    [SerializeField] private Text _countText;
    [SerializeField] private float _stepIncreaseCounter;
    [SerializeField] private float _updateInterval;

    private bool _isCounterRunning = false;
    private float _currentCount;

    private void Start()
    {
        _currentCount = _startCount;
        _countText.text = _currentCount.ToString();
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
        StartCoroutine(CountCoroutine());
    }

    private void StopCounter()
    {
        _isCounterRunning = false;
        StopCoroutine(CountCoroutine());
    }

    private IEnumerator CountCoroutine()
    {
        float previousValue = float.Parse(_countText.text);

        while (_isCounterRunning)
        {
            _currentCount += _stepIncreaseCounter;
            _countText.text = _currentCount.ToString();

            yield return new WaitForSeconds(_updateInterval);
        }
    }
}
