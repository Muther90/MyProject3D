using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private TextMeshProUGUI _countWaveText;

    private void OnEnable()
    {
        _waveManager.WaveChanged += UpdateText;
    }

    private void OnDisable()
    {
        _waveManager.WaveChanged -= UpdateText;
    }

    private void UpdateText(int current, int total)
    {
        _countWaveText.text = $"WAVE: {current} / {total}";
    }
}