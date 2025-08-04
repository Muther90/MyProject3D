using UnityEngine;

public abstract class PointProvider : MonoBehaviour
{
    [SerializeField] protected Transform[] _points;

    public abstract Vector3[] GetPoints();

    private void Awake()
    {
        if (_points == null || _points.Length == 0)
        {
            throw new System.Exception("Point(s) are not assigned!");
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    protected virtual void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _points = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            _points[i] = transform.GetChild(i);
        }
    }
#endif
}
