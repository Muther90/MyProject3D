using UnityEngine;
using UnityEngine.UI;

public class BombStatsView : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Text _statsText;

    private int _totalSpawnedCount = 0;
    private int _lastActiveCount = 0;

    private void OnEnable()
    {
        _bombSpawner.OnStateUpdated += HandleStateUpdate;
    }

    private void OnDisable()
    {
        _bombSpawner.OnStateUpdated -= HandleStateUpdate;
    }

    private void HandleStateUpdate(int activeCount, int totalCreated)
    {
        if (activeCount > _lastActiveCount)
        {
            _totalSpawnedCount += (activeCount - _lastActiveCount);
        }

        _lastActiveCount = activeCount;

        _statsText.text = $"Bombs:\n" +
                          $"Total spawned: {_totalSpawnedCount}\n" +
                          $"Created: {totalCreated}\n" +
                          $"Actives: {activeCount}\n";
    }
}