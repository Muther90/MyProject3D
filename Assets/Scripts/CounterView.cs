using UnityEngine;
using UnityEngine.UI;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Text _counterText;
    [SerializeField] private Counter _counter;

    public void UpdateCountText(float count)
    {
        _counterText.text = count.ToString();
    }

    private void OnEnable()
    {
        _counter.CountChanged += UpdateCountText;
    }

    private void OnDisable() 
    {
        _counter.CountChanged -= UpdateCountText;
    }     
}
