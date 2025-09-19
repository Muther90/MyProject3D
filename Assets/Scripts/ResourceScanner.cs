using System;
using System.Collections;
using UnityEngine;


public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _pointScan;

    private Collider[] _buffer = new Collider[100];
    private Coroutine _scanCoroutine;
    private Vector3 _pointPosition;

    public event Action<IResource> ResourceFounded;

    private void Start()
    {
        _pointPosition = _pointScan.position;
        _scanCoroutine = StartCoroutine(ScanCoroutine());
    }

    private IEnumerator ScanCoroutine()
    {
        WaitForSeconds wait = new(_delay);
        int count;

        while (enabled)
        {
            count = Physics.OverlapSphereNonAlloc(_pointPosition, _radius, _buffer);

            for (int i = 0; i < count; i++)
            {
                if (_buffer[i].TryGetComponent<IResource>(out IResource resource))
                {
                    ResourceFounded?.Invoke(resource);
                }
            }

            yield return wait;
        }

        _scanCoroutine = null;
    }
}