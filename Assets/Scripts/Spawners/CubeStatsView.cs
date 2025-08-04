using UnityEngine;
using UnityEngine.UI;

public class CubeStatsView : MonoBehaviour
{
    [SerializeField] private CubePointSpawner _cubeSpawner;
    [SerializeField] private Text _statsText;

    private int _totalSpawnedCount = 0;
    private int _lastActiveCount = 0;

    private void OnEnable()
    {
        _cubeSpawner.OnStateUpdated += HandleStateUpdate;
    }

    private void OnDisable()
    {
        _cubeSpawner.OnStateUpdated -= HandleStateUpdate;
    }

    private void HandleStateUpdate(int activeCount, int totalCreated)
    {
        if (activeCount > _lastActiveCount)
        {
            _totalSpawnedCount += (activeCount - _lastActiveCount);
        }

        _lastActiveCount = activeCount;

        _statsText.text = $"Cubes:\n" +
                          $"Total spawned: {_totalSpawnedCount}\n" +
                          $"Created: {totalCreated}\n" +
                          $"Actives: {activeCount}\n";
    }
}