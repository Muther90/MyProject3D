using DG.Tweening;
using UnityEngine;

public class RotationLoopTo : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _degree;

    private void Start()
    {
        transform.DORotate(new Vector3(0, _degree, 0), _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}