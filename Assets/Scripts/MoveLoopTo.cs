using DG.Tweening;
using UnityEngine;

public class MoveLoopTo : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _position;

    private void Start()
    {
        transform.DOMove(_position.position, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear); 
    }
}