using UnityEngine;
using UnityEngine.UI;

public class SpawnerStatsView : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawner;
    [SerializeField] private Text _statsText;
    [SerializeField] private string _nameObject;

    private int _totalSpawnedCount = 0;
    private int _lastActiveCount = 0;
    private ISpawnerStats _spawnerStats;

    private void Awake()
    {
        _spawnerStats = _spawner as ISpawnerStats;

        if (_spawnerStats == null)
        {
            throw new System.Exception("Spawner is not implements of ISpawnerStats!");
        }
    }

    private void OnEnable()
    {
        _spawnerStats.OnStateUpdated += HandleStateUpdate;
    }

    private void OnDisable()
    {
        _spawnerStats.OnStateUpdated -= HandleStateUpdate;
    }

    private void HandleStateUpdate(int activeCount, int totalCreated)
    {
        if (activeCount > _lastActiveCount)
        {
            _totalSpawnedCount += (activeCount - _lastActiveCount);
        }
        _lastActiveCount = activeCount;

        _statsText.text = $"{_nameObject}:\n" +
                          $"Total spawned: {_totalSpawnedCount}\n" +
                          $"Created: {totalCreated}\n" +
                          $"Actives: {activeCount}\n";
    }
}