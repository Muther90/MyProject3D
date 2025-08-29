using DG.Tweening;
using UnityEngine;

public class ColorLoopTo : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Color _targetColor;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        _meshRenderer.material.DOColor(_targetColor, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}