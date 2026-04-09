using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _mouseSensitivity;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _playerTransform;

    private const float MinVerticalAngle = -89f;
    private const float MaxVerticalAngle = 89f;

    private float _xRotation;

    public void Look(Vector2 lookInput)
    {
        Vector2 lookDelta = lookInput * _mouseSensitivity;

        _xRotation = Mathf.Clamp(_xRotation - lookDelta.y, MinVerticalAngle, MaxVerticalAngle);
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _playerTransform.Rotate(0f, lookDelta.x, 0f);
    }
}