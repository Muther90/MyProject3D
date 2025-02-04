using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minCountSpawnObject = 2;
    [SerializeField] private int _maxCountSpawnObject = 6;
    [SerializeField] private float _scaleSpawnObject = 0.5f;
    [SerializeField] private Clickability _clickability;

    [SerializeField] private float _splitChance = 1f;

    private void OnEnable()
    {
        _clickability.OnObjectClicked += SpawnObjects;
    }

    private void OnDisable()
    {
        _clickability.OnObjectClicked -= SpawnObjects;
    }

    private void SpawnObjects()
    {
        if (Random.value <= _splitChance)
        {
            _splitChance *= 0.5f;
            int countSpawn = Random.Range(_minCountSpawnObject, _maxCountSpawnObject);

            for (int i = 0; i < countSpawn; i++)
            {
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        GameObject newObject = Instantiate(gameObject, transform.position, transform.rotation);

        foreach (MonoBehaviour component in newObject.GetComponents<MonoBehaviour>())
        {
            component.enabled = true;
        }

        Spawner newSpawner = newObject.GetComponent<Spawner>();

        newSpawner._clickability = newObject.GetComponent<Clickability>();
        newSpawner._splitChance = this._splitChance; 

        newObject.transform.localScale = transform.localScale * _scaleSpawnObject;
    }
}
