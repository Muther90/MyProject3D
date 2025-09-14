using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private TextMeshProUGUI _resourceCountValueText;

    private void OnEnable()
    {
        _storage.ResourceValueChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _storage.ResourceValueChanged -= OnResourceCountChanged;
    }

    private void OnResourceCountChanged(int newCount)
    {
        _resourceCountValueText.text = newCount.ToString();
    }
}