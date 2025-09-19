using UnityEngine;
using TMPro;

public class StorageUI : MonoBehaviour
{
    [SerializeField] private ResourceStorage _storage;
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