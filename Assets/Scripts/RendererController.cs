using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RendererController : MonoBehaviour
{
    [SerializeField] private Color _color;

    private Renderer _renderer;
    private Material _material;
    private bool _isManagedByGroup = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    private void Start()
    {
        if (_isManagedByGroup == false)
        {
            SetColor(_color);
        }
    }

    public void TakeControl()
    {
        _isManagedByGroup = true;
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    public void SetAlpha(float alpha)
    {
        alpha = Mathf.Clamp01(alpha);
        Color currentColor = _material.color;
        currentColor.a = alpha;

        SetColor(currentColor);
    }

    public void RandomColor()
    {
        SetColor(Random.ColorHSV());
    } 
}