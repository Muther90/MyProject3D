using UnityEngine;
using UnityEngine.UI;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Text _counterText;

    public void UpdateCountText(float count)
    {
        _counterText.text = count.ToString();
    }
}
