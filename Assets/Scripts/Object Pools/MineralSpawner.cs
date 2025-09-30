using System.Collections;
using UnityEngine;

public class MineralSpawner : BaseObjectPool<Mineral>
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _radius;

    private Coroutine _generateCoroutine;

    private void Start()
    {
        _generateCoroutine = StartCoroutine(GenerateMinerals());
    }

    private IEnumerator GenerateMinerals()
    {
        WaitForSeconds wait = new(_delay);
        Vector3 spawnCenter = _spawnPoint.position;
        Vector2 randomCircle;
        Vector3 spawnPosition;
        Mineral mineral;

        while (enabled)
        {
            randomCircle = Random.insideUnitCircle * _radius;
            spawnPosition = spawnCenter + new Vector3(randomCircle.x, 0, randomCircle.y);
            mineral = Get();
            mineral.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

            yield return wait;
        }

        _generateCoroutine = null;
    }
}