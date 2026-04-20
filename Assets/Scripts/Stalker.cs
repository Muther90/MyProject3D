using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private DistanceDetector _distanceDetector;
    [SerializeField] private Mover _mover;

    private void FixedUpdate()
    {
        if (_distanceDetector.TargetDetected == false)
        {
            _mover.MoveTo(_distanceDetector.Target - transform.position);
        }
    }
}