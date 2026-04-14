using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private TextMeshProUGUI _countWaveText;

    private void OnEnable()
    {
        _waveController.WaveChanged += UpdateText;
    }

    private void OnDisable()
    {
        _waveController.WaveChanged -= UpdateText;
    }

    private void UpdateText(int current, int total)
    {
        _countWaveText.text = $"WAVE: {current} / {total}";
    }
}