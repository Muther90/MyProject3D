using UnityEngine;
using UnityEngine.UI;

public class PushForward : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private Button _button;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _rigidbody.AddForce(Vector3.forward * _force);
    }
}