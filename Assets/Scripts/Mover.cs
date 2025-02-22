using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Router _router;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToTarget;

    private Vector3[] _waypoints;
    private Vector3 _currentTarget;
    private int _waypointIndex = 0;
    private float _squaredDistanceToTarget;

    private void Start()
    {
        if (_router == null)
        {
            throw new System.Exception("Router not assigned!");
        }

        _waypoints = _router.GetWaypoints();

        _squaredDistanceToTarget = _distanceToTarget * _distanceToTarget;
        _currentTarget = _waypoints[_waypointIndex];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        if ((transform.position - _currentTarget).sqrMagnitude <= _squaredDistanceToTarget)
        {
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        _waypointIndex = ++_waypointIndex % _waypoints.Length;
        _currentTarget = _waypoints[_waypointIndex];
        transform.forward = (_currentTarget - transform.position).normalized;
    }
}
