using UnityEngine;

public class WeaponRaycaster : MonoBehaviour
{
    private const float ViewCenterX = 0.5f;
    private const float ViewCenterY = 0.5f;

    [SerializeField] private Camera _camera;

    public void PerformRaycast(float damage, float range)
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(ViewCenterX, ViewCenterY, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}