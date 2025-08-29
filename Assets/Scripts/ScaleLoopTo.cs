using DG.Tweening;
using UnityEngine;

public class ScaleLoopTo : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _scale;

    void Start()
    {
        transform.DOScale(new Vector3(_scale, _scale, _scale), _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}