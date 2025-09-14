using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _targetColor;

    private Renderer _renderer;

    private void OnValidate()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _targetColor;
    }
}