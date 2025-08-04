using System.Collections.Generic;
using UnityEngine;

public class RandomRadius : PointProvider
{
    [SerializeField] private float _radius;
    [SerializeField] private int _maxRandomPoints;

    private List<Vector3> _randomPoints = new();

    public override Vector3[] GetPoints()
    {
        foreach (Transform point in _points)
        {
            for (int i = 0; i < _maxRandomPoints; i++)
            {
                _randomPoints.Add(point.position + Random.insideUnitSphere * _radius);
            }
        }

        return _randomPoints.ToArray();
    }
}