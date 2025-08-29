using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLoopText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _duration;
    [SerializeField] private string _targetText;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_text.DOText(_targetText, _duration));
        sequence.Append(_text.DOText(_targetText, _duration).SetRelative());
        sequence.Append(_text.DOText(_targetText, _duration, true, ScrambleMode.All));

        sequence.Play().SetLoops(-1, LoopType.Yoyo);
    }
}