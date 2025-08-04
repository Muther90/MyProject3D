using UnityEngine;

public class GroupRendererController : MonoBehaviour
{
    [SerializeField] private RendererController[] _rendererControllers;
    [SerializeField] private Color _groupColor;

    public float Alpha => _groupColor.a;

    private void Awake()
    {
        foreach (RendererController controller in _rendererControllers)
        {
            if (controller != null)
            {
                controller.TakeControl();
            }
        }
    }

    private void Start()
    {
        SetColorForAll(_groupColor);
    }

    public void SetColorForAll(Color color)
    {
        _groupColor = color;

        foreach (RendererController controller in _rendererControllers)
        {
            if (controller != null)
            {
                controller.SetColor(color);
            }
        }
    }

    public void SetAlphaForAll(float alpha)
    {
        alpha = Mathf.Clamp01(alpha);
        _groupColor.a = alpha;

        foreach (RendererController controller in _rendererControllers)
        {
            if (controller != null)
            {
                controller.SetAlpha(alpha);
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child RendererControllers Array")]
    private void RefreshRendererControllersArray()
    {
        _rendererControllers = GetComponentsInChildren<RendererController>();
    }
#endif
}
