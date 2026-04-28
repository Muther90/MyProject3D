using UnityEngine;
using UnityEngine.UI;

public class Catapult : MonoBehaviour
{
    [SerializeField] private Transform _projectileTransform;
    [SerializeField] private Rigidbody _projectileRigidbody;
    [SerializeField] private Transform _spoonLoadPoint;
    [SerializeField] private Rigidbody _spoonRigidbody;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private Button _buttonFire;
    [SerializeField] private Button _buttonReload;

    private float _spring;

    private void Awake()
    {
        _spring = _springJoint.spring;
        Reload();
    }

    private void OnEnable()
    {
        _buttonFire.onClick.AddListener(Fire);
        _buttonReload.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        _buttonFire.onClick.RemoveListener(Fire);
        _buttonReload.onClick.RemoveListener(Reload);
    }

    private void Fire()
    {
        _spoonRigidbody.WakeUp();
        _springJoint.spring = _spring;
    }

    private void Reload()
    {
        _springJoint.spring = 0;

        _projectileRigidbody.velocity = Vector3.zero;
        _projectileTransform.position = _spoonLoadPoint.position;
        _projectileTransform.rotation = _spoonLoadPoint.rotation;
    }
}