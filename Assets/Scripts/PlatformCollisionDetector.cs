using UnityEngine;

public class CubeDetector : CollisionDetector<Platform>
{
    [SerializeField] private Cube _cube;

    protected override void OnComponentDetected()
    {
        _cube.OnPlatformCollision();
    }
}
